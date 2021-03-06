﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CRMAPI.Infrastructure.DataAccess;
using CRMAPI.Infrastructure.Repository;
using CRMAPI.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CRM_API.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.WithOrigins("http://localhost:4200")
                                                          .AllowAnyMethod()
                                                          .AllowAnyHeader()
                                                          .AllowCredentials()
                                                           ));
            services.AddDbContext<CRMProviderDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper();
            services.AddTransient<IContactService, ContactService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Audience = Configuration["AzureAD:ClientID"];
                        options.Authority= $"https://login.microsoftonline.com/tfp/{Configuration["AzureAD:Tenant"]}/{Configuration["AzureAD:Policy"]}/v2.0/";
                        // options.Authority = $"{Configuration["AzureAD:AadInstance"]}{Configuration["AzureAD:Tenant"]}";
                    });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "CRM API", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRM API");
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

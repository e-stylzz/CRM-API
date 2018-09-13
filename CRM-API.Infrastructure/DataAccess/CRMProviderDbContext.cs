using System;
using Microsoft.EntityFrameworkCore;
using CRMAPI.Infrastructure.Configuration;
using CRMAPI.Domain.Models;

namespace CRMAPI.Infrastructure.DataAccess
{
    public class CRMProviderDbContext : DbContext
    {
        public CRMProviderDbContext(DbContextOptions<CRMProviderDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new ContactConfiguration(modelBuilder.Entity<Contact>());
        }
    }
}

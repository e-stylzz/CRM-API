using System;
using CRMAPI.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMAPI.Infrastructure.Configuration
{
    public class ContactConfiguration
    {
        public ContactConfiguration(EntityTypeBuilder<Contact> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.FirstName).HasMaxLength(30);
            entityBuilder.Property(t => t.LastName).HasMaxLength(30);

            entityBuilder.HasData(
                new Contact { Id = Guid.Parse("4A310BE4-8852-4536-8E88-E0C9BF7A3857"), FirstName = "Bo", LastName = "Jangles" }
            );
        }
    }
}

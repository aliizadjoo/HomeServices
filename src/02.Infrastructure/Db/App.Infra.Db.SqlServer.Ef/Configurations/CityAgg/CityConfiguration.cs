using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.CityAgg
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.City)
                .HasForeignKey(o => o.CityId)
                .OnDelete(DeleteBehavior.NoAction );

            builder.HasMany(c => c.Customers)
                .WithOne(customer => customer.City)
                .HasForeignKey(customer => customer.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.Experts)
               .WithOne(e => e.City)
               .HasForeignKey(e => e.CityId)
               .OnDelete(DeleteBehavior.NoAction);

                builder.HasData(
                new City { Id = 1, Name = "تهران", CreatedAt = new DateTime(2025, 1, 1) },
                new City { Id = 2, Name = "کرج", CreatedAt = new DateTime(2025, 1, 1) },
                new City { Id = 3, Name = "شیراز", CreatedAt = new DateTime(2025, 1, 1) },
                new City { Id = 4, Name = "اصفهان", CreatedAt = new DateTime(2025, 1, 1) }
               );

            
        }
    }
}

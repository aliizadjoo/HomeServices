using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.CustomerAgg
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(c => c.AppUser)
                .WithOne(a=>a.CustomerProfile)
                .HasForeignKey<Customer>(c=>c.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);



            builder.HasMany(c=> c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(c => c.Reviews)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.HasData(new Customer
            {
                Id = 1,
                AppUserId = 2,
                Address = "تهران، خیابان آزادی، پلاک ۱",
                CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0),
                IsDeleted = false
            });

        }
    }
}

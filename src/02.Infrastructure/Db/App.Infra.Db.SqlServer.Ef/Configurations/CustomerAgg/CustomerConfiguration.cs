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
                .WithOne(a => a.CustomerProfile)
                .HasForeignKey<Customer>(c => c.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);



            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(c => c.Reviews)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.WalletBalance)
                 .HasPrecision(18, 2)
                 .IsRequired();


            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.HasData(

     new Customer
     {
         Id = 1,
         AppUserId = 2,
         CityId = 1,
         WalletBalance = 100000m,
         Address = "تهران، خیابان آزادی، پلاک ۱",
         CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0),
         IsDeleted = false
     },

     new Customer
     {
         Id = 2,
         AppUserId = 4,
         CityId = 1,
         WalletBalance = 50000m,
         Address = "تهران، سعادت آباد، خیابان سرو",
         CreatedAt = new DateTime(2025, 1, 15),
         IsDeleted = false
     },
     new Customer
     {
         Id = 3,
         AppUserId = 5,
         CityId = 2,
         WalletBalance = 250000m,
         Address = "تبریز، ولیعصر، خیابان استانداری",
         CreatedAt = new DateTime(2025, 2, 01),
         IsDeleted = false
     },
     new Customer
     {
         Id = 4,
         AppUserId = 6,
         CityId = 3,
         WalletBalance = 0m,
         Address = "مشهد، بلوار سجاد، کوچه بهار",
         CreatedAt = new DateTime(2025, 2, 20),
         IsDeleted = false
     },
     new Customer
     {
         Id = 5,
         AppUserId = 7,
         CityId = 1,
         WalletBalance = 120000m,
         Address = "تهران، فلکه دوم صادقیه",
         CreatedAt = new DateTime(2025, 3, 05),
         IsDeleted = false
     },
     new Customer
     {
         Id = 6,
         AppUserId = 8,
         CityId = 4,
         WalletBalance = 75000m,
         Address = "شیراز، خیابان عفیف آباد، مجتمع ستاره",
         CreatedAt = new DateTime(2025, 3, 12),
         IsDeleted = false
     }
 );

        }
    }
}

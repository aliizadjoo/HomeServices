using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.AppUserAgg
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(a => a.Id);
            builder.ToTable("AppUsers");

            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(a => a.CustomerProfile)
                .WithOne(c => c.AppUser)
                .HasForeignKey<Customer>(c => c.AppUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.ExpertProfile)
                .WithOne(e => e.AppUser)
                 .HasForeignKey<Expert>(c => c.AppUserId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.AdminProfile)
                .WithOne(ad => ad.AppUser)
                 .HasForeignKey<Admin>(c => c.AppUserId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.NoAction);




            var adminUser = new AppUser
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "System",
                UserName = "admin@site.com",
                NormalizedUserName = "ADMIN@SITE.COM",
                Email = "admin@site.com",
                NormalizedEmail = "ADMIN@SITE.COM",
                LockoutEnabled = false,
                EmailConfirmed = true
                ,
                PasswordHash = "AQAAAAIAAYagAAAAEL4Tp49DzswqZ6q7mepXL9QgUeLu2a79cBvt7ur6nUGpKZ1dFdTkUAAiZR+TtArxfQ=="
            };
            

            var customerUser = new AppUser
            {
                Id = 2,
                FirstName = "Ali",
                LastName = "Moshtari",
                UserName = "customer@site.com",
                NormalizedUserName = "CUSTOMER@SITE.COM",
                Email = "customer@site.com",
                NormalizedEmail = "CUSTOMER@SITE.COM",
                LockoutEnabled = false,
                EmailConfirmed = true
                ,
                PasswordHash = "AQAAAAIAAYagAAAAEL4Tp49DzswqZ6q7mepXL9QgUeLu2a79cBvt7ur6nUGpKZ1dFdTkUAAiZR+TtArxfQ=="
            };
           

            var expertUser = new AppUser
            {
                Id = 3,
                FirstName = "Reza",
                LastName = "Karshenas",
                UserName = "expert@site.com",
                NormalizedUserName = "EXPERT@SITE.COM",
                Email = "expert@site.com",
                NormalizedEmail = "EXPERT@SITE.COM",
                LockoutEnabled = false,
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEL4Tp49DzswqZ6q7mepXL9QgUeLu2a79cBvt7ur6nUGpKZ1dFdTkUAAiZR+TtArxfQ=="

            };
         

            builder.HasData(adminUser, customerUser, expertUser);
        }
    }
}

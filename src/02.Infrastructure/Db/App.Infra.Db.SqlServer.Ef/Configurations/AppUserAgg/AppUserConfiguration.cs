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
                PasswordHash = "AQAAAAIAAYagAAAAEL4Tp49DzswqZ6q7mepXL9QgUeLu2a79cBvt7ur6nUGpKZ1dFdTkUAAiZR+TtArxfQ==",
                SecurityStamp = "B6FE5F0E-18B5-4062-AEF8-11555793E7CB",
                ConcurrencyStamp = "081004b3-e25c-4414-9694-44e5c2fd863f"
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
                SecurityStamp = "D4D09FBB-ED60-4E17-B03E-B9B4B6C70E5D",
                ConcurrencyStamp = "df5dbb19-753c-4cc7-a623-13c8508d00f8",
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
                SecurityStamp = "9489458F-27BA-400D-A45F-AFCE3D9A8D26",
                ConcurrencyStamp = "58afcc4e-2a82-4ad6-8e12-665778173973",
                PasswordHash = "AQAAAAIAAYagAAAAEL4Tp49DzswqZ6q7mepXL9QgUeLu2a79cBvt7ur6nUGpKZ1dFdTkUAAiZR+TtArxfQ=="

            };
         

            builder.HasData(adminUser, customerUser, expertUser);
        }
    }
}

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




            var allUsers = new List<AppUser>
{
    // Admin User
    new AppUser
    {
        Id = 1, FirstName = "Admin", LastName = "System", UserName = "admin@site.com", NormalizedUserName = "ADMIN@SITE.COM",
        Email = "admin@site.com", NormalizedEmail = "ADMIN@SITE.COM", EmailConfirmed = true, LockoutEnabled = false,
        PasswordHash = "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==",
        SecurityStamp = "B6FE5F0E-18B5-4062-AEF8-11555793E7CB", ConcurrencyStamp = "081004b3-e25c-4414-9694-44e5c2fd863f",
        ImagePath = "admin.jpg"
    },

    // Initial Customer & Expert
    new AppUser
    {
        Id = 2, FirstName = "Ali", LastName = "Moshtari", UserName = "customer@site.com", NormalizedUserName = "CUSTOMER@SITE.COM",
        Email = "customer@site.com", NormalizedEmail = "CUSTOMER@SITE.COM", EmailConfirmed = true, LockoutEnabled = false,
        PasswordHash = "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==",
        SecurityStamp = "D4D09FBB-ED60-4E17-B03E-B9B4B6C70E5D", ConcurrencyStamp = "df5dbb19-753c-4cc7-a623-13c8508d00f8",
        ImagePath = "customer1.jpg"
    },
    new AppUser
    {
        Id = 3, FirstName = "Reza", LastName = "Karshenas", UserName = "expert@site.com", NormalizedUserName = "EXPERT@SITE.COM",
        Email = "expert@site.com", NormalizedEmail = "EXPERT@SITE.COM", EmailConfirmed = true, LockoutEnabled = false,
        PasswordHash = "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==",
        SecurityStamp = "9489458F-27BA-400D-A45F-AFCE3D9A8D26", ConcurrencyStamp = "58afcc4e-2a82-4ad6-8e12-665778173973",
        ImagePath = "expert1.jpg"
    },

    // Customer List
    new AppUser { Id = 4, FirstName = "Zahra", LastName = "Ahmadi", UserName = "customer2@site.com", NormalizedUserName = "CUSTOMER2@SITE.COM", Email = "customer2@site.com", NormalizedEmail = "CUSTOMER2@SITE.COM", EmailConfirmed = true, SecurityStamp = "S2-68B5-4062-AEF8", ConcurrencyStamp = "C2-04b3-e25c-4414", PasswordHash = "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", ImagePath = "customer2.jpg" },
    new AppUser { Id = 5, FirstName = "Mohammad", LastName = "Hosseini", UserName = "customer3@site.com", NormalizedUserName = "CUSTOMER3@SITE.COM", Email = "customer3@site.com", NormalizedEmail = "CUSTOMER3@SITE.COM", EmailConfirmed = true, SecurityStamp = "S3-68B5-4062-AEF8", ConcurrencyStamp = "C3-04b3-e25c-4414", PasswordHash = "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", ImagePath = "customer3.jpg" },
    new AppUser { Id = 6, FirstName = "Maryam", LastName = "Moradi", UserName = "customer4@site.com", NormalizedUserName = "CUSTOMER4@SITE.COM", Email = "customer4@site.com", NormalizedEmail = "CUSTOMER4@SITE.COM", EmailConfirmed = true, SecurityStamp = "S4-68B5-4062-AEF8", ConcurrencyStamp = "C4-04b3-e25c-4414", PasswordHash = "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", ImagePath = "customer4.jpg" },
    new AppUser { Id = 7, FirstName = "Saeed", LastName = "Karimi", UserName = "customer5@site.com", NormalizedUserName = "CUSTOMER5@SITE.COM", Email = "customer5@site.com", NormalizedEmail = "CUSTOMER5@SITE.COM", EmailConfirmed = true, SecurityStamp = "S5-68B5-4062-AEF8", ConcurrencyStamp = "C5-04b3-e25c-4414", PasswordHash = "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", ImagePath = "customer5.jpg" },
    new AppUser { Id = 8, FirstName = "Niloufar", LastName = "Sadeghi", UserName = "customer6@site.com", NormalizedUserName = "CUSTOMER6@SITE.COM", Email = "customer6@site.com", NormalizedEmail = "CUSTOMER6@SITE.COM", EmailConfirmed = true, SecurityStamp = "S6-68B5-4062-AEF8", ConcurrencyStamp = "C6-04b3-e25c-4414", PasswordHash = "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", ImagePath = "customer6.jpg" },

    // Expert List
    new AppUser { Id = 9, FirstName = "Hassan", LastName = "Alavi", UserName = "expert2@site.com", NormalizedUserName = "EXPERT2@SITE.COM", Email = "expert2@site.com", NormalizedEmail = "EXPERT2@SITE.COM", EmailConfirmed = true, SecurityStamp = "E2-68B5-4062-AEF8", ConcurrencyStamp = "CE2-04b3-e25c-4414", PasswordHash = "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", ImagePath = "expert2.jpg" },
    new AppUser { Id = 10, FirstName = "Sara", LastName = "Mousavi", UserName = "expert3@site.com", NormalizedUserName = "EXPERT3@SITE.COM", Email = "expert3@site.com", NormalizedEmail = "EXPERT3@SITE.COM", EmailConfirmed = true, SecurityStamp = "E3-68B5-4062-AEF8", ConcurrencyStamp = "CE3-04b3-e25c-4414", PasswordHash = "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", ImagePath = "expert3.jpg" },
    new AppUser { Id = 11, FirstName = "Omid", LastName = "Rahmani", UserName = "expert4@site.com", NormalizedUserName = "EXPERT4@SITE.COM", Email = "expert4@site.com", NormalizedEmail = "EXPERT4@SITE.COM", EmailConfirmed = true, SecurityStamp = "E4-68B5-4062-AEF8", ConcurrencyStamp = "CE4-04b3-e25c-4414", PasswordHash = "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", ImagePath = "expert4.jpg" },
    new AppUser { Id = 12, FirstName = "Elham", LastName = "Jafari", UserName = "expert5@site.com", NormalizedUserName = "EXPERT5@SITE.COM", Email = "expert5@site.com", NormalizedEmail = "EXPERT5@SITE.COM", EmailConfirmed = true, SecurityStamp = "E5-68B5-4062-AEF8", ConcurrencyStamp = "CE5-04b3-e25c-4414", PasswordHash = "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", ImagePath = "expert5.jpg" },
    new AppUser { Id = 13, FirstName = "Meysam", LastName = "Ghasemi", UserName = "expert6@site.com", NormalizedUserName = "EXPERT6@SITE.COM", Email = "expert6@site.com", NormalizedEmail = "EXPERT6@SITE.COM", EmailConfirmed = true, SecurityStamp = "E6-68B5-4062-AEF8", ConcurrencyStamp = "CE6-04b3-e25c-4414", PasswordHash = "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", ImagePath = "expert6.jpg" }
};

            builder.HasData(allUsers);

        }
    }
}

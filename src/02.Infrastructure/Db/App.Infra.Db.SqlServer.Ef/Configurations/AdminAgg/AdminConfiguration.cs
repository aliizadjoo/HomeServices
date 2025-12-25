using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.AdminAgg
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.StaffCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(a => a.AppUser)
                .WithOne(ap=>ap.AdminProfile)
                .HasForeignKey<Admin>(a => a.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(a => a.TotalRevenue)
                    .HasPrecision(18, 2)
                    .IsRequired();

            builder.HasQueryFilter(a => !a.IsDeleted);

            builder.HasData(new Admin
            {
                Id = 1,
                AppUserId = 1,
                TotalRevenue = 0m,
                StaffCode = "ADM-1001",
                CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0),
                IsDeleted = false
            });
        }
    }
}

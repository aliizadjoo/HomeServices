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
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StaffCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(x => x.AppUser)
                .WithOne(a=>a.AdminProfile)
                .HasForeignKey<Admin>(x => x.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(a => !a.IsDeleted);

            builder.HasData(new Admin
            {
                Id = 1,
                AppUserId = 1,
                StaffCode = "ADM-1001",
                CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0),
                IsDeleted = false
            });
        }
    }
}

using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

            builder.HasOne(a=>a.CustomerProfile)
                .WithOne(c => c.AppUser)
                .HasForeignKey<Customer>(c => c.AppUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.ExpertProfile)
                .WithOne(e=>e.AppUser)
                 .HasForeignKey<Expert>(c => c.AppUserId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.AdminProfile)
                .WithOne(ad => ad.AppUser)
                 .HasForeignKey<Admin>(c => c.AppUserId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

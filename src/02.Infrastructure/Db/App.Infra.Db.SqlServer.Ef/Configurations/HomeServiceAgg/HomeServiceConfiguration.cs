using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.HomeServiceAgg
{
    public class HomeServiceConfiguration : IEntityTypeConfiguration<HomeService>
    {
        public void Configure(EntityTypeBuilder<HomeService> builder)
        {
            builder.ToTable("HomeServices");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(h => h.ImagePath)
               .IsRequired();

            builder.Property(h => h.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(h => h.BasePrice)
           .HasPrecision(18, 2)
           .IsRequired();


            builder.HasOne(hs => hs.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(hs => hs.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(hs=>hs.Orders)
                .WithOne(o=>o.HomeService)
                .HasForeignKey(o=>o.HomeServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(hs => !hs.IsDeleted);

            builder.HasMany(hs => hs.Experts)
               .WithMany(e => e.HomeServices);

            
            builder.HasData(
        new HomeService
        {
            Id = 1,
            Name = "نظافت منزل",
            Description = "نظافت کامل فضاهای داخلی ساختمان",
            ImagePath = "cleaning-service.jpg",
            BasePrice = 500000m,
            CategoryId = 1,
            CreatedAt = new DateTime(2025, 1, 1),
            IsDeleted = false
        },
        new HomeService
        {
            Id = 2,
            Name = "تعمیر کولر آبی",
            Description = "سرویس دوره‌ای و تعمیر موتور کولر",
            ImagePath = "cooler-repair.jpg",
            BasePrice = 300000m,
            CategoryId = 2,
            CreatedAt = new DateTime(2025, 1, 1),
            IsDeleted = false
        }
    );

        }
    }
}

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

            builder.HasMany(hs => hs.ExpertHomeServices)
               .WithOne(e => e.HomeService);


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
        Name = "تعمیر کولر ",
        Description = "سرویس دوره‌ای و تعمیر موتور کولر",
        ImagePath = "cooler-repair.jpg",
        BasePrice = 300000m,
        CategoryId = 2,
        CreatedAt = new DateTime(2025, 1, 1),
        IsDeleted = false
    },

   
    new HomeService
    {
        Id = 3,
        Name = "تعمیر یخچال و فریزر",
        Description = "عیب‌یابی و شارژ گاز انواع یخچال‌های ایرانی و خارجی",
        ImagePath = "fridge-repair.jpg",
        BasePrice = 800000m,
        CategoryId = 3, 
        CreatedAt = new DateTime(2025, 1, 10),
        IsDeleted = false
    },
    new HomeService
    {
        Id = 4,
        Name = "کارواش در محل",
        Description = "شستشوی کامل بدنه و داخل خودرو با نانو بدون آب",
        ImagePath = "carwash-service.jpg",
        BasePrice = 250000m,
        CategoryId = 4,
        CreatedAt = new DateTime(2025, 1, 12),
        IsDeleted = false
    },
    new HomeService
    {
        Id = 5,
        Name = "اصلاح سر و صورت",
        Description = "خدمات آرایشی مردانه و زنانه در منزل شما",
        ImagePath = "barber-service.jpg",
        BasePrice = 400000m,
        CategoryId = 5, 
        CreatedAt = new DateTime(2025, 1, 15),
        IsDeleted = false
    },
    new HomeService
    {
        Id = 6,
        Name = "بسته‌بندی و اسباب‌کشی",
        Description = "جمع‌آوری وسایل و حمل اثاثیه با کادر مجرب",
        ImagePath = "moving-service.jpg",
        BasePrice = 1500000m,
        CategoryId = 6, 
        CreatedAt = new DateTime(2025, 1, 18),
        IsDeleted = false
    },
    new HomeService
    {
        Id = 7,
        Name = "هرس درختان و گل‌کاری",
        Description = "رسیدگی به باغچه و طراحی فضای سبز",
        ImagePath = "gardening-service.jpg",
        BasePrice = 600000m,
        CategoryId = 7, 
        CreatedAt = new DateTime(2025, 1, 20),
        IsDeleted = false
    }
);

        }
    }
}

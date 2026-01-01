using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.CategoryAgg
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("Categories");

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.ImagePath)
                .IsRequired();


            builder.HasMany(c=>c.Services)
                .WithOne(h=>h.Category)
                .HasForeignKey(h=>h.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.HasData(
                  new Category { Id = 1, Title = "نظافت و پذیرایی", ImagePath = "cleaning-category.png", CreatedAt = new DateTime(2025, 1, 1), IsDeleted = false },
                  new Category { Id = 2, Title = "تعمیرات و تأسیسات", ImagePath = "repairs-category.png", CreatedAt = new DateTime(2025, 1, 1), IsDeleted = false },
                  new Category { Id = 3, Title = "تعمیرات لوازم خانگی", ImagePath = "appliances-category.png", CreatedAt = new DateTime(2025, 1, 5), IsDeleted = false },
                  new Category { Id = 4, Title = "خدمات خودرو", ImagePath = "car-services-category.png", CreatedAt = new DateTime(2025, 1, 10), IsDeleted = false },
                  new Category { Id = 5, Title = "آرایش و زیبایی", ImagePath = "beauty-category.png", CreatedAt = new DateTime(2025, 1, 15), IsDeleted = false },
                  new Category { Id = 6, Title = "اسباب‌کشی و حمل و نقل", ImagePath = "moving-category.png", CreatedAt = new DateTime(2025, 1, 20), IsDeleted = false },
                  new Category { Id = 7, Title = "باغبانی و فضای سبز", ImagePath = "gardening-category.png", CreatedAt = new DateTime(2025, 1, 25), IsDeleted = false }
              );


        }
    }
}

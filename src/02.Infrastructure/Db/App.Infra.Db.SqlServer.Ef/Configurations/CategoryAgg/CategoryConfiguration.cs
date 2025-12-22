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
                .IsRequired(false);


            builder.HasMany(c=>c.Services)
                .WithOne(h=>h.Category)
                .HasForeignKey(h=>h.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.HasData(
                  new Category { Id = 1, Title = "نظافت و پذیرایی", CreatedAt = new DateTime(2025, 1, 1), IsDeleted = false },
                  new Category { Id = 2, Title = "تعمیرات و تأسیسات", CreatedAt = new DateTime(2025, 1, 1), IsDeleted = false }
              );


        }
    }
}

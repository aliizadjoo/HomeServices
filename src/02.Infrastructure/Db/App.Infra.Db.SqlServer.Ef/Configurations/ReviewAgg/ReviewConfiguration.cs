using App.Domain.Core.Entities;
using App.Domain.Core.Enums.ReviewAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.ReviewAgg
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");


            builder.HasKey(r => r.Id);

            builder.Property(r => r.Comment)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(r => r.Score)
                .IsRequired();

            builder.HasOne(r => r.Order)
                .WithOne(o => o.Review)
                .HasForeignKey<Review>(r => r.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);



            builder.HasOne(r => r.Expert)
                .WithMany(e => e.Reviews)
                .HasForeignKey(r => r.ExpertId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(r => !r.IsDeleted);



            builder.HasData(

     new Review
     {
         Id = 1,
         OrderId = 1,
         CustomerId = 1,
         ExpertId = 1,
         Comment = "نظافت بسیار دقیق و منظم انجام شد. کاملاً راضی هستم.",
         Score = 5,
         CreatedAt = new DateTime(2025, 2, 2),
         IsDeleted = false,
         ReviewStatus=ReviewStatus.Approved
     },

     new Review
     {
         Id = 2,
         OrderId = 3,
         CustomerId = 3,
         ExpertId = 2,
         Comment = "سرویس کولر خوب بود ولی کمی با تاخیر مراجعه کردند.",
         Score = 4,
         CreatedAt = new DateTime(2025, 1, 22),
         IsDeleted = false,
         ReviewStatus = ReviewStatus.Approved
     },

     new Review
     {
         Id = 3,
         OrderId = 5,
         CustomerId = 5,
         ExpertId = 5,
         Comment = "باغبانی عالی و حرفه‌ای! حیاط ما کاملاً متحول شد.",
         Score = 5,
         CreatedAt = new DateTime(2025, 3, 5),
         IsDeleted = false,
         ReviewStatus = ReviewStatus.Approved
     },

     new Review
     {
         Id = 4,
         OrderId = 7,
         CustomerId = 2,
         ExpertId = 1,
         Comment = "کار تمیز و به‌موقع انجام شد.",
         Score = 5,
         CreatedAt = new DateTime(2025, 3, 8),
         IsDeleted = false,
         ReviewStatus = ReviewStatus.Approved
     },

     new Review
     {
         Id = 5,
         OrderId = 8,
         CustomerId = 4,
         ExpertId = 4,
         Comment = "برخورد محترمانه و کیفیت مناسب.",
         Score = 4,
         CreatedAt = new DateTime(2025, 3, 12),
         IsDeleted = false,
         ReviewStatus = ReviewStatus.Approved
     },

     new Review
     {
         Id = 6,
         OrderId = 9,
         CustomerId = 6,
         ExpertId = 6,
         Comment = "تعمیرکار کاملاً مسلط بود و مشکل حل شد.",
         Score = 5,
         CreatedAt = new DateTime(2025, 3, 18),
         IsDeleted = false,
         ReviewStatus = ReviewStatus.Approved
     },

     new Review
     {
         Id = 7,
         OrderId = 10,
         CustomerId = 1,
         ExpertId = 2,
         Comment = "سرویس کامل انجام شد، پیشنهاد می‌کنم.",
         Score = 4,
         CreatedAt = new DateTime(2025, 3, 20),
         IsDeleted = false,
         ReviewStatus = ReviewStatus.Approved
     },

     new Review
     {
         Id = 8,
         OrderId = 11,
         CustomerId = 3,
         ExpertId = 5,
         Comment = "طراحی بسیار زیبا و خلاقانه بود.",
         Score = 5,
         CreatedAt = new DateTime(2025, 3, 22),
         IsDeleted = false,
         ReviewStatus = ReviewStatus.Approved
     },

     new Review
     {
         Id = 9,
         OrderId = 12,
         CustomerId = 5,
         ExpertId = 6,
         Comment = "مشکل یخچال کاملاً برطرف شد.",
         Score = 5,
         CreatedAt = new DateTime(2025, 3, 25),
         IsDeleted = false,
         ReviewStatus = ReviewStatus.Approved
     },

     new Review
     {
         Id = 10,
         OrderId = 13,
         CustomerId = 2,
         ExpertId = 1,
         Comment = "سریع و حرفه‌ای، راضی بودم.",
         Score = 4,
         CreatedAt = new DateTime(2025, 3, 28),
         IsDeleted = false,
         ReviewStatus = ReviewStatus.Approved
     }
 );




        }
    }
}

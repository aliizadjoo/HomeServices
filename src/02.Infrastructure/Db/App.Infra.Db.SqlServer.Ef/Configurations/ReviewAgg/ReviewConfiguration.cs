using App.Domain.Core.Entities;
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
        OrderId = 3,
        CustomerId = 3,
        ExpertId = 2, 
        Comment = "سرویس کولر خیلی خوب انجام شد، فقط کمی با تاخیر آمدند.",
        Score = 4,
        CreatedAt = new DateTime(2025, 1, 22),
        IsDeleted = false
    },

    
    new Review
    {
        Id = 2,
        OrderId = 5,
        CustomerId = 5,
        ExpertId = 5, 
        Comment = "باغبانی عالی و حرفه‌ای! حیاط ما کاملاً متحول شد. ممنونم از خانم جعفری.",
        Score = 5,
        CreatedAt = new DateTime(2025, 3, 5),
        IsDeleted = false
    },

  
    new Review
    {
        Id = 3,
        OrderId = 1,
        CustomerId = 1,
        ExpertId = 1,
        Comment = "بسیار تمیز و با حوصله کار انجام شد. راضی بودم.",
        Score = 5,
        CreatedAt = new DateTime(2025, 2, 2),
        IsDeleted = false
    }
);


        }
    }
}

using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.ExpertAgg
{
    public class ExpertConfiguration : IEntityTypeConfiguration<Expert>
    {
        public void Configure(EntityTypeBuilder<Expert> builder)
        {
            builder.ToTable("Experts");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Bio)
                .IsRequired(false)
                .HasMaxLength(2000);

     

            builder.HasOne(e => e.AppUser)
                .WithOne(e=>e.ExpertProfile)
                .HasForeignKey<Expert>(e => e.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.ExpertHomeServices)
                 .WithOne(hs => hs.Expert);
                
                builder.HasMany(e => e.Proposals)
                       .WithOne(p => p.Expert)
                       .HasForeignKey(p=>p.ExpertId)
                       .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.WalletBalance)
                   .HasPrecision(18, 2)
                   .IsRequired(false);

            builder.HasMany(e => e.Reviews)
                .WithOne(r=>r.Expert)
                .HasForeignKey(r=>r.ExpertId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(e => !e.IsDeleted);
            builder.HasData(
                
                new Expert
                {
                    Id = 1,
                    AppUserId = 3,
                    Bio = "متخصص در امور فنی با ۱۰ سال سابقه کار",
                    WalletBalance = 200000m,
                    CityId = 1,
                    AverageScore =4.7,
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0),
                    IsDeleted = false
                },
                
                new Expert
                {
                    Id = 2,
                    AppUserId = 9,
                    Bio = "کارشناس ارشد تاسیسات و سیستم‌های برودتی",
                    WalletBalance = 500000m,
                    CityId = 1,
                    AverageScore= 4,
                    CreatedAt = new DateTime(2025, 2, 10),
                    IsDeleted = false
                },
                new Expert
                {
                    Id = 3,
                    AppUserId = 10,
                    Bio = "متخصص طراحی داخلی و دکوراسیون با مدرک بین‌المللی",
                    WalletBalance = 1200000m,
                    CityId = 2,
                 
                    CreatedAt = new DateTime(2025, 3, 15),
                    IsDeleted = false
                },
                new Expert
                {
                    Id = 4,
                    AppUserId = 11,
                    Bio = "تکنسین برق قدرت و هوشمندسازی منازل",
                    WalletBalance = 0m,
                    CityId = 1,
                    AverageScore = 4,
                    CreatedAt = new DateTime(2025, 4, 05),
                    IsDeleted = false
                },
                new Expert
                {
                    Id = 5,
                    AppUserId = 12,
                    Bio = "متخصص باغبانی و فضای سبز",
                    WalletBalance = 350000m,
                    CityId = 3,
                    AverageScore = 5,
                    CreatedAt = new DateTime(2025, 5, 20),
                    IsDeleted = false
                },
                new Expert
                {
                    Id = 6,
                    AppUserId = 13,
                    Bio = "کارشناس تعمیرات لوازم خانگی دیجیتال",
                    WalletBalance = 800000m,
                    CityId = 2,
                    CreatedAt = new DateTime(2025, 6, 12),
                    AverageScore=5,
                    IsDeleted = false
                }
            );

        }
    }
}

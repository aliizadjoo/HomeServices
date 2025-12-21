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
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(e => e.ProfilePicture)
                .IsRequired(false);

            builder.HasOne(e => e.AppUser)
                .WithOne(e=>e.ExpertProfile)
                .HasForeignKey<Expert>(e => e.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Skills)
                 .WithMany(hs => hs.Experts);
                
                builder.HasMany(e => e.Proposals)
                       .WithOne(p => p.Expert)
                       .HasForeignKey(p=>p.ExpertId)
                       .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(e => e.Reviews)
                .WithOne(r=>r.Expert)
                .HasForeignKey(r=>r.ExpertId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(new Expert
            {
                Id = 1,
                AppUserId = 3,
                Bio = "متخصص در امور فنی با ۱۰ سال سابقه کار",
                ProfilePicture = "expert-profile.jpg",
                CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0),
                IsDeleted = false
            });


        }
    }
}

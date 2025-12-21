using App.Domain.Core.Entities;
using App.Domain.Core.Enums.ProposalAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.ProposalAgg
{
    public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            builder.ToTable("Proposals");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.ProposedPrice)
            .HasPrecision(18, 2)
            .IsRequired();

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(2000);


            builder.Property(p => p.Status)
                .IsRequired()
                .HasDefaultValue(ProposalStatus.Pending);


            builder.HasOne(p => p.Order)
                .WithMany(o => o.Proposals)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Expert)
                .WithMany(e => e.Proposals)
                .HasForeignKey(p => p.ExpertId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasData(new Proposal
            {
                Id = 1,
                OrderId = 1,
                ExpertId = 1,
                ProposedPrice = 550000m,
                Description = "با تجهیزات کامل نظافتی در زمان تعیین شده حضور خواهم یافت.",
                Status = ProposalStatus.Pending,
                CreatedAt = new DateTime(2025, 1, 11),
                IsDeleted = false
            });

        }
    }
}

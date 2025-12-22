using App.Domain.Core.Entities;
using App.Domain.Core.Enums.OrderAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.OrderAgg
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(o=>o.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(o => o.Status)
                .IsRequired()
                .HasDefaultValue(OrderStatus.WaitingForProposals);


            builder.Property(o => o.ExecutionDate)
                .IsRequired();

            builder.Property(o => o.ExecutionTime)
                .IsRequired();

            builder.HasOne(o=>o.Customer)
                .WithMany(c=>c.Orders)
                .HasForeignKey(o=>o.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o=>o.HomeService)
                .WithMany(h=>h.Orders)
                .HasForeignKey(o=>o.HomeServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(o=>o.Proposals)
                .WithOne(p=>p.Order)
                .HasForeignKey(p=>p.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o=>o.Review)
                .WithOne(r=>r.Order)
                .HasForeignKey<Review>(r=>r.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(o => !o.IsDeleted);


            builder.HasData(new Order
            {
                Id = 1,
                CustomerId = 1,
                HomeServiceId = 1,
                Description = "نظافت آپارتمان ۸۰ متری، دو خوابه",
                Status = OrderStatus.WaitingForProposals,
                ExecutionDate = new DateTime(2025, 2, 1),
                ExecutionTime = new TimeSpan(10, 0, 0), 
                CreatedAt = new DateTime(2025, 1, 10),
                IsDeleted = false
            });

        }
    }
}

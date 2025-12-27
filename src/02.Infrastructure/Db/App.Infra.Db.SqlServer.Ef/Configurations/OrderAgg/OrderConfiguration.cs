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

            builder.Property(o => o.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(o => o.Status)
                .IsRequired()
                .HasDefaultValue(OrderStatus.WaitingForProposals);


            builder.Property(o => o.ExecutionDate)
                .IsRequired();

            builder.Property(o => o.ExecutionTime)
                .IsRequired();

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.HomeService)
                .WithMany(h => h.Orders)
                .HasForeignKey(o => o.HomeServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(o => o.Proposals)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Review)
                .WithOne(r => r.Order)
                .HasForeignKey<Review>(r => r.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(o => o.Images)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(o => !o.IsDeleted);


            builder.HasData(

       new Order
       {
           Id = 1,
           CustomerId = 1,
           HomeServiceId = 1,
           CityId = 1,
           Description = "نظافت آپارتمان ۸۰ متری، دو خوابه",
           Status = OrderStatus.Finished,
           ExecutionDate = new DateTime(2025, 2, 1),
           ExecutionTime = new TimeSpan(10, 0, 0),
           CreatedAt = new DateTime(2025, 1, 10),
           IsDeleted = false
       },

       new Order
       {
           Id = 2,
           CustomerId = 2,
           HomeServiceId = 3,
           CityId = 1,
           Description = "یخچال ساید بای ساید صدای ناهنجار می‌دهد",
           Status = OrderStatus.WaitingForSelection,
           ExecutionDate = new DateTime(2025, 2, 5),
           ExecutionTime = new TimeSpan(14, 30, 0),
           CreatedAt = new DateTime(2025, 1, 15),
           IsDeleted = false
       },

       new Order
       {
           Id = 3,
           CustomerId = 3,
           HomeServiceId = 2,
           CityId = 2,
           Description = "سرویس کامل کولر آبی برای فصل جدید",
           Status = OrderStatus.Finished,
           ExecutionDate = new DateTime(2025, 1, 20),
           ExecutionTime = new TimeSpan(9, 0, 0),
           CreatedAt = new DateTime(2025, 1, 5),
           IsDeleted = false
       },

       new Order
       {
           Id = 4,
           CustomerId = 4,
           HomeServiceId = 4,
           CityId = 3,
           Description = "شستشوی کامل پژو ۲۰۶ در پارکینگ منزل",
           Status = OrderStatus.Cancelled,
           ExecutionDate = new DateTime(2025, 2, 10),
           ExecutionTime = new TimeSpan(16, 0, 0),
           CreatedAt = new DateTime(2025, 1, 20),
           IsDeleted = false
       },

       new Order
       {
           Id = 5,
           CustomerId = 5,
           HomeServiceId = 7,
           CityId = 1,
           Description = "هرس درختان حیاط و کاشت گل‌های فصلی",
           Status = OrderStatus.Finished,
           ExecutionDate = new DateTime(2025, 3, 1),
           ExecutionTime = new TimeSpan(8, 0, 0),
           CreatedAt = new DateTime(2025, 2, 10),
           IsDeleted = false
       },

       new Order
       {
           Id = 6,
           CustomerId = 6,
           HomeServiceId = 6,
           CityId = 4,
           Description = "جابجایی اثاثیه به ساختمان مجاور، طبقه سوم با آسانسور",
           Status = OrderStatus.Started,
           ExecutionDate = new DateTime(2025, 3, 15),
           ExecutionTime = new TimeSpan(11, 0, 0),
           CreatedAt = new DateTime(2025, 2, 20),
           IsDeleted = false
       }
   );

        }
    }
}

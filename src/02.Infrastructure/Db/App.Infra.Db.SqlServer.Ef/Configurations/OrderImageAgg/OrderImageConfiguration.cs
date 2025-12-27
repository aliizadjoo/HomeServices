using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.OrderImageAgg
{
    public class OrderImageConfiguration : IEntityTypeConfiguration<OrderImage>
    {
        public void Configure(EntityTypeBuilder<OrderImage> builder)
        {
            builder.ToTable("OrderImages");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.ImagePath)
                .IsRequired()
                .HasMaxLength(500);


            builder.HasData(
    
    new OrderImage { Id = 1, OrderId = 1, ImagePath = "apartment-cleaning-1.jpg", CreatedAt = new DateTime(2025, 1, 10, 11, 0, 0), IsDeleted = false },
    new OrderImage { Id = 2, OrderId = 1, ImagePath = "apartment-cleaning-2.jpg", CreatedAt = new DateTime(2025, 1, 10, 11, 0, 0), IsDeleted = false },

   
    new OrderImage
    {
        Id = 3,
        OrderId = 2, 
        ImagePath = "fridge-issue-photo.jpg",
        CreatedAt = new DateTime(2025, 1, 15, 10, 30, 0),
        IsDeleted = false
    },
    new OrderImage
    {
        Id = 4,
        OrderId = 3, 
        ImagePath = "cooler-on-roof.jpg",
        CreatedAt = new DateTime(2025, 1, 20, 09, 15, 0),
        IsDeleted = false
    },
    new OrderImage
    {
        Id = 5,
        OrderId = 4,
        ImagePath = "dirty-car-front.jpg",
        CreatedAt = new DateTime(2025, 1, 21, 16, 0, 0),
        IsDeleted = false
    },
    new OrderImage
    {
        Id = 6,
        OrderId = 5, 
        ImagePath = "garden-yard-view.jpg",
        CreatedAt = new DateTime(2025, 2, 10, 12, 0, 0),
        IsDeleted = false
    },
    new OrderImage
    {
        Id = 7,
        OrderId = 6, 
        ImagePath = "moving-boxes.jpg",
        CreatedAt = new DateTime(2025, 2, 22, 14, 45, 0),
        IsDeleted = false
    }
);

        }
    }
}

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
                     new OrderImage
                     {
                         Id = 1,
                         OrderId = 1,
                         ImagePath = "apartment-cleaning-1.jpg",
                         CreatedAt = new DateTime(2025, 1, 10, 11, 0, 0),
                         IsDeleted = false
                     },
                     new OrderImage
                     {
                         Id = 2,
                         OrderId = 1,
                         ImagePath = "apartment-cleaning-2.jpg",
                         CreatedAt = new DateTime(2025, 1, 10, 11, 0, 0),
                         IsDeleted = false
                            }
                        );

            }
    } 
}

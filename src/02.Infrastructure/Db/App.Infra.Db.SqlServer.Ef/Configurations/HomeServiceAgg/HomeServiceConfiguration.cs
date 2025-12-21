using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.HomeServiceAgg
{
    public class HomeServiceConfiguration : IEntityTypeConfiguration<HomeService>
    {
        public void Configure(EntityTypeBuilder<HomeService> builder)
        {
            builder.ToTable("HomeServices");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(h => h.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(h => h.BasePrice)
           .HasPrecision(18, 2)
           .IsRequired();


            builder.HasOne(h => h.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(h=>h.Orders)
                .WithOne(o=>o.HomeService)
                .HasForeignKey(o=>o.HomeServiceId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}

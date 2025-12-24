using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.RoleAgg
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
        {
            builder.HasData(
                 new IdentityRole<int>
                 {
                     Id = 1,
                     Name = "Admin",
                     NormalizedName = "ADMIN",
                     ConcurrencyStamp = "78641951-8712-4261-9B5A-431A29D67A41" 
                 },
                 new IdentityRole<int>
                 {
                     Id = 2,
                     Name = "Customer",
                     NormalizedName = "CUSTOMER",
                     ConcurrencyStamp = "45127812-1234-5678-9B5A-431A29D67A42"
                 },
                 new IdentityRole<int>
                 {
                     Id = 3,
                     Name = "Expert",
                     NormalizedName = "EXPERT",
                     ConcurrencyStamp = "12345678-8712-4261-9B5A-431A29D67A43"
                 }
             );
        }
    }
}

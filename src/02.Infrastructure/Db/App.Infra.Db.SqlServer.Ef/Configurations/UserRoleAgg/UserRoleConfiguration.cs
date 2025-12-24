using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.UserRoleAgg
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder.HasData(
                 
                 new IdentityUserRole<int> { UserId = 1, RoleId = 1 },

                
                 new IdentityUserRole<int> { UserId = 2, RoleId = 2 },

                 new IdentityUserRole<int> { UserId = 3, RoleId = 3 }
             );
        }
    }
}

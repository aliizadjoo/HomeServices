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

                 new IdentityUserRole<int> { UserId = 3, RoleId = 3 },

                 new IdentityUserRole<int> { UserId = 4, RoleId = 2 },
                 new IdentityUserRole<int> { UserId = 5, RoleId = 2 },
                 new IdentityUserRole<int> { UserId = 6, RoleId = 2 },
                 new IdentityUserRole<int> { UserId = 7, RoleId = 2 },
                 new IdentityUserRole<int> { UserId = 8, RoleId = 2 },

                 new IdentityUserRole<int> { UserId = 9, RoleId = 3 },
                 new IdentityUserRole<int> { UserId = 10, RoleId = 3 },
                 new IdentityUserRole<int> { UserId = 11, RoleId = 3 },
                 new IdentityUserRole<int> { UserId = 12, RoleId = 3 },
                 new IdentityUserRole<int> { UserId = 13, RoleId = 3 }

             );
        }
    }
}

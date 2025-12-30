using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.ExpertHomeServiceAgg
{
    public class ExpertHomeServiceConfiguration : IEntityTypeConfiguration<ExpertHomeService>
    {
        public void Configure(EntityTypeBuilder<ExpertHomeService> builder)
        {
            builder.HasKey(ehs => ehs.Id);
            builder.HasQueryFilter(ehs => !ehs.IsDeleted);
        }
    }
}

using App.Domain.Core._common;
using App.Domain.Core.Dtos.AdminAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.AdminAgg.AppService
{
    public interface IAdminAppService
    {
        public Task<Result<AdminPagedResultDto>> GetAll( int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}

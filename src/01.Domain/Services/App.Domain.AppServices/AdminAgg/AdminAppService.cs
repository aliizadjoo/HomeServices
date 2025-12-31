using App.Domain.Core._common;
using App.Domain.Core.Contract.AdminAgg.AppService;
using App.Domain.Core.Contract.AdminAgg.Service;
using App.Domain.Core.Dtos.AdminAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.AdminAgg
{
    public class AdminAppService(IAdminService _adminService) : IAdminAppService
    {
        public async Task<Result<AdminPagedResultDto>> GetAll( int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _adminService.GetAll( pageNumber, pageSize, cancellationToken);
        }
    }
}

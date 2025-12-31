using App.Domain.Core._common;
using App.Domain.Core.Contract.ExpertAgg.AppService;
using App.Domain.Core.Contract.ExpertAgg.Service;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.ExpertAgg
{
    public class ExpertAppService
        (IExpertService _expertService
        ,ILogger<ExpertAppService> _logger ) : IExpertAppService
    {
        public async Task<Result<ProfileExpertDto>> GetProfile(int appuserId, CancellationToken cancellationToken)
        {

             _logger.LogInformation("[AppService] شروع فرآیند دریافت پروفایل برای کارشناس {ExpertId}", appuserId);
             return await _expertService.GetProfile(appuserId, cancellationToken);
        }
        public async Task<Result<bool>> ChangeProfile(int appuserId, ProfileExpertDto profileExpertDto, CancellationToken cancellationToken)
        {
            return await _expertService.ChangeProfile(appuserId, profileExpertDto, cancellationToken);
        }

        public async Task<Result<ExpertPagedResultDto>> GetAll( int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
           return await _expertService.GetAll( pageNumber, pageSize, cancellationToken);
        }
    }
}

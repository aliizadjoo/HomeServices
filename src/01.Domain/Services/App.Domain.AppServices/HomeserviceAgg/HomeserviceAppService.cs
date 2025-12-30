using App.Domain.Core._common;
using App.Domain.Core.Contract.HomeServiceAgg.Service;
using App.Domain.Core.Dtos.HomeServiceAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.HomeserviceAgg
{
    public class HomeserviceAppService
        (IHomeserviceService _homeserviceService) : IHomeserviceAppService
    {
        public async Task<Result<List<HomeserviceSummaryDto>>> GetAll(CancellationToken cancellationToken)
        {
            return await _homeserviceService.GetAll(cancellationToken);
        }
    }
}

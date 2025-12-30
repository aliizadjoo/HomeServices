using App.Domain.Core._common;
using App.Domain.Core.Contract.HomeServiceAgg.Repository;
using App.Domain.Core.Contract.HomeServiceAgg.Service;
using App.Domain.Core.Dtos.HomeServiceAgg;
using App.Domain.Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.HomeserviceServiceAgg
{
    public class HomeserviceService(IHomeserviceRepository _homeserviceRepository ,
        ILogger<HomeserviceService> _logger) : IHomeserviceService
    {
        public async Task<Result<List<HomeserviceSummaryDto>>> GetAll( CancellationToken cancellationToken)
        {
             var homeServiceSummaryDto = await  _homeserviceRepository.GetAll(cancellationToken);
            if (homeServiceSummaryDto == null || !homeServiceSummaryDto.Any())
            {
                _logger.LogWarning("هیچ سرویس فعالی در سیستم یافت نشد.");
                return Result<List<HomeserviceSummaryDto>>.Failure("در حال حاضر هیچ سرویسی برای نمایش وجود ندارد.");
            }
            return Result<List<HomeserviceSummaryDto>>.Success(homeServiceSummaryDto);
        }
    }
}

using App.Domain.Core.Contract.CityAgg.Repository;
using App.Domain.Core.Contract.CityAgg.Service;
using App.Domain.Core.Dtos.CityAgg;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.CityAgg
{
    public class CityService(ICityRepository _cityRepository,
        ILogger<CityService>  _logger) : ICityService

    {
        public async Task<List<CityDto>> GetAll(CancellationToken cancellationToken)
        {
            _logger.LogInformation("درخواست برای دریافت لیست تمام شهرها ارسال شد.");
            return await _cityRepository.GetAll(cancellationToken);
        }
    }
}

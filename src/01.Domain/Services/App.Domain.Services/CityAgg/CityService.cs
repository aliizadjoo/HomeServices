using App.Domain.Core.Contract.CityAgg.Repository;
using App.Domain.Core.Contract.CityAgg.Service;
using App.Domain.Core.Dtos.CityAgg;
using App.Infra.Cache;
using App.Infra.Cache.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.CityAgg
{
    public class CityService(ICityRepository _cityRepository, ICityRepositoryDapper _cityRepositoryDapper,
        ILogger<CityService>  _logger , ICacheService _cacheService) : ICityService
    {
        public async Task<List<CityDto>> GetAll(CancellationToken cancellationToken)
        {
            _logger.LogInformation("درخواست برای دریافت لیست تمام شهرها ارسال شد.");

            var cachedCities=_cacheService.Get<List<CityDto>>(CacheKeys.Cities);

            if (cachedCities==null)
            {
               var cities=await _cityRepositoryDapper.GetAll(cancellationToken);
               _cacheService.SetSliding<List<CityDto>>(CacheKeys.Cities , cities , 30);
               return cities;
            }
            return  cachedCities;
        }

        public async Task<bool> IsExist(int CityId, CancellationToken cancellationToken)
        {
            return await _cityRepository.IsExist(CityId, cancellationToken);
        }
    }
}

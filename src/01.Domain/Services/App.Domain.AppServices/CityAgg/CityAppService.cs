using App.Domain.Core.Contract.CityAgg.AppService;
using App.Domain.Core.Contract.CityAgg.Service;
using App.Domain.Core.Dtos.CityAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.CityAgg
{
    public class CityAppService(ICityService _cityService) : ICityAppService
    {
        public async Task<List<CityDto>> GetAll(CancellationToken cancellationToken)
        {
           return await  _cityService.GetAll(cancellationToken);
        }
    }
}

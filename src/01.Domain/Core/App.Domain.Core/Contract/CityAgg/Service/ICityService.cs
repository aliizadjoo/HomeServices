using App.Domain.Core.Dtos.CityAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CityAgg.Service
{
    public interface ICityService
    {
        public Task<List<CityDto>> GetAll(CancellationToken cancellationToken);
        public Task<bool> IsExist(int CityId, CancellationToken cancellationToken);
    }
}

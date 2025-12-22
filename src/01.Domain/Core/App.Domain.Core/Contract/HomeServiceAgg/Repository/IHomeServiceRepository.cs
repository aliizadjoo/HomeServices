using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Dtos.HomeServiceAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.HomeServiceAgg.Repository
{
    public interface IHomeServiceRepository
    {
        public Task<int> Create(CreateHomeServiceDto homeServiceDto, CancellationToken cancellationToken);
    
        public Task<int> Update(HomeServiceDto homeServiceDto, CancellationToken cancellationToken);
        public Task<List<HomeServiceDto>> GetAll(int pageSize , int pageNumber, SearchHomeServiceDto search, CancellationToken cancellationToken);

        public Task<HomeServiceDto?> GetById(int homeServiceId, CancellationToken cancellationToken);

    }
}

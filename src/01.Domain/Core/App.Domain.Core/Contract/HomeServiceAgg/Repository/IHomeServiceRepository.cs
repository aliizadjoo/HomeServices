using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Dtos.HomeServiceAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.HomeServiceAgg.Repository
{
    public interface IHomeserviceRepository
    {
        public Task<int> Create(CreateHomeServiceDto homeServiceDto, CancellationToken cancellationToken);

        public Task<bool> Update(HomeserviceDto dto, CancellationToken cancellationToken);
        public Task<List<HomeserviceDto>> GetAll(int pageSize , int pageNumber, SearchHomeServiceDto search, CancellationToken cancellationToken);

        public Task<HomeserviceDto?> GetById(int homeServiceId, CancellationToken cancellationToken);


        public Task<List<HomeserviceSummaryDto>> GetAll( CancellationToken cancellationToken);

       public Task<int> GetCount(CancellationToken cancellationToken);

        public Task<bool> Delete(int id, CancellationToken cancellationToken);
      




    }
}

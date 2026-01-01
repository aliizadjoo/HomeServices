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
    public interface IHomeserviceAppService
    {
        public Task<Result<List<HomeserviceSummaryDto>>> GetAll(CancellationToken cancellationToken);

        public Task<Result<HomeservicePagedDto>> GetAll(int pageSize, int pageNumber, SearchHomeServiceDto search, CancellationToken cancellationToken);

        

        public Task<Result<int>> Create(CreateHomeServiceDto homeServiceDto, CancellationToken cancellationToken);

        public Task<Result<bool>> Update(HomeserviceDto dto, CancellationToken cancellationToken);

        public Task<Result<HomeserviceDto>> GetById(int id, CancellationToken cancellationToken);

        public  Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);
        


    }
}

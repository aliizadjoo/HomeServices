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
        public async Task<Result<int>> Create(CreateHomeServiceDto homeServiceDto, CancellationToken cancellationToken)
        {
           return await  _homeserviceService.Create(homeServiceDto, cancellationToken);
        }

        public async Task<Result<List<HomeserviceDto>>> GetAll(CancellationToken cancellationToken)
        {
            return await _homeserviceService.GetAll(cancellationToken);
        }

        public async Task<Result<HomeservicePagedDto>> GetAll(int pageSize, int pageNumber, SearchHomeServiceDto search, CancellationToken cancellationToken)
        {
            return await _homeserviceService.GetAll(pageSize, pageNumber, search, cancellationToken);
        }


        public async Task<Result<bool>> Update(HomeserviceDto dto, CancellationToken cancellationToken)
        {
           return await _homeserviceService.Update(dto, cancellationToken);
        }

        public async Task<Result<HomeserviceDto>> GetById(int id, CancellationToken cancellationToken)
        {
            return await _homeserviceService.GetById(id, cancellationToken);
        }

        public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
        {
            return await _homeserviceService.Delete(id, cancellationToken);
        }

        public async Task<Result<HomeservicePagedDto>> GetServicesByCategoryId(int categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
          return await  _homeserviceService.GetServicesByCategoryId(categoryId, pageNumber, pageSize, cancellationToken);
        }

        public async Task<Result<HomeservicePagedDto>> GetAll(int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
          return await _homeserviceService.GetAll(pageSize , pageNumber , cancellationToken);
        }
    }
}

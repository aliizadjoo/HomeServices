using App.Domain.Core._common;
using App.Domain.Core.Contract.HomeServiceAgg.Repository;
using App.Domain.Core.Contract.HomeServiceAgg.Service;
using App.Domain.Core.Dtos.HomeServiceAgg;
using App.Domain.Core.Entities;
using App.Infra.Cache;
using App.Infra.Cache.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.HomeserviceServiceAgg
{
    public class HomeserviceService(IHomeserviceRepository _homeserviceRepository ,
        ILogger<HomeserviceService> _logger , ICacheService _cacheService) : IHomeserviceService
    {
        public async Task<Result<List<HomeserviceDto>>> GetAll( CancellationToken cancellationToken)
        {
            var homeServiceDto = await  _homeserviceRepository.GetAll(cancellationToken);
            if (homeServiceDto == null || !homeServiceDto.Any())
            {
                _logger.LogWarning("هیچ سرویس فعالی در سیستم یافت نشد.");
                return Result<List<HomeserviceDto>>.Failure("در حال حاضر هیچ سرویسی برای نمایش وجود ندارد.");
            }
            return Result<List<HomeserviceDto>>.Success(homeServiceDto);
        }

        public async Task<Result<HomeservicePagedDto>> GetAll(int pageSize, int pageNumber, SearchHomeServiceDto search, CancellationToken cancellationToken)
        {
            _logger.LogInformation("دریافت لیست سرویس‌ها - صفحه {Page}", pageNumber);

            var services = await _homeserviceRepository.GetAll( cancellationToken);

            if (services==null || !services.Any())
            {
                return Result<HomeservicePagedDto>.Failure("خدمتی یافت نشد.");
            }

            IEnumerable <HomeserviceDto> filteredHomeserviceSummaryDto = services;

            if (!string.IsNullOrWhiteSpace(search.Name))
            {
                filteredHomeserviceSummaryDto = filteredHomeserviceSummaryDto.Where(hs => hs.Name.Contains(search.Name));
            }
            if (search.CategoryId>0)
            {
                filteredHomeserviceSummaryDto = filteredHomeserviceSummaryDto.Where(hs => hs.CategoryId==search.CategoryId);
            }

            var totalCount = filteredHomeserviceSummaryDto.Count();
            var pagedData = filteredHomeserviceSummaryDto
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize).ToList();   


            return Result<HomeservicePagedDto>.Success(new HomeservicePagedDto 
            {
              HomeserviceDtos = pagedData 
              , TotalCount = totalCount
            });
        }


        public async Task<Result<HomeservicePagedDto>> GetAll(int pageSize, int pageNumber,  CancellationToken cancellationToken)
        {
            _logger.LogInformation("دریافت لیست سرویس‌ها - صفحه {Page}", pageNumber);

            var services = await _homeserviceRepository.GetAll(cancellationToken);

            if (services == null || !services.Any())
            {
                return Result<HomeservicePagedDto>.Failure("خدمتی یافت نشد.");
            }

            IEnumerable<HomeserviceDto> filteredHomeserviceSummaryDto = services;

          

            var totalCount = filteredHomeserviceSummaryDto.Count();
            var pagedData = filteredHomeserviceSummaryDto
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();


            return Result<HomeservicePagedDto>.Success(new HomeservicePagedDto
            {
                HomeserviceDtos = pagedData
              ,
                TotalCount = totalCount
            });
        }

        public async Task<Result<int>> Create(CreateHomeServiceDto homeServiceDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("درخواست ایجاد سرویس جدید با نام: {Name}", homeServiceDto.Name);

            
            var result = await _homeserviceRepository.Create(homeServiceDto, cancellationToken);

            if (result > 0)
            {
                _cacheService.Remove(CacheKeys.Homeservices);
                return Result<int>.Success(result, "سرویس جدید با موفقیت ثبت شد.");
            }

            return Result<int>.Failure("خطایی در هنگام ذخیره سرویس رخ داد.");
        }

        public async Task<Result<bool>> Update(HomeserviceDto dto, CancellationToken cancellationToken)
        {
            var isUpdated = await _homeserviceRepository.Update(dto, cancellationToken);

            if (isUpdated)
            {
                _cacheService.Remove(CacheKeys.Homeservices);
                return Result<bool>.Success(true, "سرویس با موفقیت بروزرسانی شد.");
            }

            return Result<bool>.Failure("عملیات بروزرسانی با شکست مواجه شد. سرویس مورد نظر یافت نشد.");
        }


        public async Task<Result<HomeserviceDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var homeService = await _homeserviceRepository.GetById(id, cancellationToken);

            if (homeService == null)
            {
                return Result<HomeserviceDto>.Failure("سرویس مورد نظر یافت نشد.");
            }

            return Result<HomeserviceDto>.Success(homeService);
        }


        public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _homeserviceRepository.Delete(id, cancellationToken);

            if (isDeleted)
            {
                _cacheService.Remove(CacheKeys.Homeservices);
                return Result<bool>.Success(true, "سرویس مورد نظر با موفقیت حذف  شد.");
            }

            return Result<bool>.Failure("عملیات حذف شکست خورد. .");
        }


        public async Task<Result<HomeservicePagedDto>> GetServicesByCategoryId(int categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var data = await _homeserviceRepository.GetServicesByCategoryId(categoryId, pageNumber, pageSize, cancellationToken);

            if (data.HomeserviceDtos == null || !data.HomeserviceDtos.Any())
                return Result<HomeservicePagedDto>.Failure("سرویسی در این دسته‌بندی یافت نشد.");

            return Result<HomeservicePagedDto>.Success(data);
        }
    }
}

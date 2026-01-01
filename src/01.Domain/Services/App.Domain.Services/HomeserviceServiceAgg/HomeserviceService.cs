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


        public async Task<Result<HomeservicePagedDto>> GetAll(int pageSize, int pageNumber, SearchHomeServiceDto search, CancellationToken cancellationToken)
        {
            _logger.LogInformation("دریافت لیست سرویس‌ها - صفحه {Page}", pageNumber);

            var services = await _homeserviceRepository.GetAll(pageSize, pageNumber, search, cancellationToken);

            if (services.HomeserviceDtos==null || !services.HomeserviceDtos.Any())
            {
                return Result<HomeservicePagedDto>.Failure("خدمتی یافت نشد.");
            }
            return Result<HomeservicePagedDto>.Success(services);
        }

       

        public async Task<Result<int>> Create(CreateHomeServiceDto homeServiceDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("درخواست ایجاد سرویس جدید با نام: {Name}", homeServiceDto.Name);

            
            var result = await _homeserviceRepository.Create(homeServiceDto, cancellationToken);

            if (result > 0)
            {
                return Result<int>.Success(result, "سرویس جدید با موفقیت ثبت شد.");
            }

            return Result<int>.Failure("خطایی در هنگام ذخیره سرویس رخ داد.");
        }

        public async Task<Result<bool>> Update(HomeserviceDto dto, CancellationToken cancellationToken)
        {
            var isUpdated = await _homeserviceRepository.Update(dto, cancellationToken);

            if (isUpdated)
            {
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
                return Result<bool>.Success(true, "سرویس مورد نظر با موفقیت حذف  شد.");
            }

            return Result<bool>.Failure("عملیات حذف شکست خورد. .");
        }
    }
}

using App.Domain.Core._common;
using App.Domain.Core.Contract.CityAgg.Repository;
using App.Domain.Core.Contract.CustomerAgg.Repository;
using App.Domain.Core.Contract.CustomerAgg.Service;
using App.Domain.Core.Dtos.AccountAgg;
using App.Domain.Core.Dtos.CustomerAgg;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.CustomerAgg
{
    public class CustomerService
        (ICustomerRepository _customerRepository ,
        ICityRepository _cityRepository,
        ILogger<CustomerService> _logger) : ICustomerService

    {
        public async Task<Result<bool>> ChangeProfileCustomer(int appuserId, ProfileCustomerDto profileCustomerDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("شروع فرآیند ویرایش پروفایل برای مشتری {Id}", appuserId);

           
            var isCityValid = await _cityRepository.IsExist(profileCustomerDto.CityId, cancellationToken);

            if (!isCityValid)
            {
                _logger.LogWarning("شهر با کد {CityId} در سیستم یافت نشد.", profileCustomerDto.CityId);
                return Result<bool>.Failure("شهر انتخاب شده معتبر نیست.");
            }

            
            var isUpdated = await _customerRepository.ChangeProfileCustomer(appuserId, profileCustomerDto, cancellationToken);

            if (!isUpdated)
            {
                _logger.LogError("عملیات ویرایش در دیتابیس با شکست مواجه شد. مشتری: {Id}", appuserId);
                return Result<bool>.Failure("ویرایش پروفایل انجام نشد.خطایی رخ داده است..");
            }

          
            _logger.LogInformation("پروفایل مشتری {Id} با موفقیت ویرایش شد.", appuserId);
            return Result<bool>.Success(true, "اطلاعات پروفایل شما با موفقیت بروزرسانی شد.");
        }

        public async Task<Result<ProfileCustomerDto>> GetProfileCustomer(int appuserId, CancellationToken cancellationToken)
        {
            var ProfileCustomerDto = await _customerRepository.GetProfileCustomer(appuserId, cancellationToken);
            if (ProfileCustomerDto==null)
            {
                _logger.LogWarning("مشتری با کد شناسایی {CustomerId} در سیستم یافت نشد.", appuserId);
                return Result<ProfileCustomerDto>.Failure("مشتری ای با این کد شناسایی یافت نشد.");
            }

            return Result<ProfileCustomerDto>.Success(ProfileCustomerDto);
        }

        
        public async Task<Result<bool>> Create(int userId, int cityId, CancellationToken cancellationToken)
        {
            CreateCustomerDto createCustmoerDto = new CreateCustomerDto()
            {
                AppUserId = userId,
                CityId = cityId
            };

            var result = await _customerRepository.Create(createCustmoerDto, cancellationToken);

            if (result<=0)
                return Result<bool>.Failure("خطا در ایجاد پروفایل مشتری");

            return Result<bool>.Success(true);
        }
    }
}

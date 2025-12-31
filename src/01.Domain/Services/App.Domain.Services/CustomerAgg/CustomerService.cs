using App.Domain.Core._common;
using App.Domain.Core.Contract.CityAgg.Repository;
using App.Domain.Core.Contract.CustomerAgg.Repository;
using App.Domain.Core.Contract.CustomerAgg.Service;
using App.Domain.Core.Dtos.AccountAgg;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
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
        UserManager<AppUser> _userManager,
        ILogger<CustomerService> _logger) : ICustomerService

    {
        public async Task<Result<bool>> ChangeProfileCustomer(int appuserId, ProfileCustomerDto profileCustomerDto, bool isAdmin, CancellationToken cancellationToken)
        {
           _logger.LogInformation("شروع ویرایش پروفایل برای کاربر {Id}. نقش ویرایش‌کننده: {Role}", appuserId, isAdmin ? "Admin" : "Customer"); 

    
            var isCityValid = await _cityRepository.IsExist(profileCustomerDto.CityId, cancellationToken);
            if (!isCityValid)
            {
                return Result<bool>.Failure("شهر انتخاب شده معتبر نیست.");
            }

            if (isAdmin && !string.IsNullOrEmpty(profileCustomerDto.Email))
            {
                
                var user = await _userManager.FindByIdAsync(appuserId.ToString());
                if (user != null && user.Email != profileCustomerDto.Email)
                {
                    user.Email = profileCustomerDto.Email;
                    user.NormalizedEmail = profileCustomerDto.Email.ToUpper();
                    user.UserName = profileCustomerDto.Email;
                    user.NormalizedUserName = profileCustomerDto.Email.ToUpper();
                    user.EmailConfirmed = true;

                    var identityResult = await _userManager.UpdateAsync(user);
                    if (!identityResult.Succeeded)
                    {
                        return Result<bool>.Failure("خطا در بروزرسانی ایمیل توسط سیستم احراز هویت.");
                    }
                }
            }

          
            var isUpdated = await _customerRepository.ChangeProfileCustomer(appuserId, profileCustomerDto, isAdmin, cancellationToken);

            if (!isUpdated)
            {
                _logger.LogError("شکست در بروزرسانی فیلدهای دیتابیس برای کاربر {Id}", appuserId);
                 return Result<bool>.Failure("خطایی در ذخیره‌سازی اطلاعات رخ داد.");
            }

            return Result<bool>.Success(true, "ویرایش با موفقیت انجام شد.");
        }

        public async Task<Result<ProfileCustomerDto>> GetProfileCustomerByAppUserId(int appuserId, CancellationToken cancellationToken)
        {
            var ProfileCustomerDto = await _customerRepository.GetProfileCustomer(appuserId, cancellationToken);
            if (ProfileCustomerDto==null)
            {
                _logger.LogWarning("مشتری با کد شناسایی {CustomerId} در سیستم یافت نشد.", appuserId);
                return Result<ProfileCustomerDto>.Failure("مشتری ای با این کد شناسایی یافت نشد.");
            }

            return Result<ProfileCustomerDto>.Success(ProfileCustomerDto);
        }

        
        


        public async Task<Result<CustomerPagedResultDto>> GetAll(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
          
            var customers = await _customerRepository.GetAll( pageNumber, pageSize, cancellationToken);

         
            if (customers == null || !customers.Customers.Any())
            {
                return Result<CustomerPagedResultDto>.Failure("هیچ مشتری با مشخصات وارد شده یافت نشد.");
            }

            return Result<CustomerPagedResultDto>.Success(customers);
        }



        public async Task<Result<bool>> Create(int userId, int cityId, CancellationToken cancellationToken)
        {
            CreateCustomerDto createCustmoerDto = new CreateCustomerDto()
            {
                AppUserId = userId,
                CityId = cityId
            };

            var result = await _customerRepository.Create(createCustmoerDto, cancellationToken);

            if (result <= 0)
                return Result<bool>.Failure("خطا در ایجاد پروفایل مشتری");

            return Result<bool>.Success(true);
        }


        public async Task<Result<bool>> DeleteUser(int appUserId, CancellationToken cancellationToken)
        {
           var result= await _customerRepository.DeleteUser(appUserId, cancellationToken);
            if (!result)
            {
                return Result<bool>.Failure("خطا در حذف مشتری");
            }

            return Result<bool>.Success(true);
        }
    }
}

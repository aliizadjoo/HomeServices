using App.Domain.Core._common;
using App.Domain.Core.Contract.AccountAgg.Services;
using App.Domain.Core.Contract.CityAgg.Repository;
using App.Domain.Core.Contract.ExpertAgg.Repository;
using App.Domain.Core.Contract.ExpertAgg.Service;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.ExpertAgg
{
    public class ExpertService
        (IExpertRepository _expertRepository
        , ICityRepository _cityRepository ,
         UserManager<AppUser> _userManager
        , ILogger<ExpertService> _logger,
         IAccountService _accountService
        ) : IExpertService
    {
        public async Task<Result<bool>> Create(int userId, int cityId, CancellationToken cancellationToken)
        {
            CreateExpertDto createExperterDto = new CreateExpertDto()
            {
                AppUserId = userId,
                CityId = cityId
            };

            var result = await _expertRepository.Create(createExperterDto, cancellationToken);

            if (result <= 0)
                return Result<bool>.Failure("خطا در ایجاد پروفایل مشتری");

            return Result<bool>.Success(true);
        }

        public async Task<Result<ProfileExpertDto>> GetProfile(int appuserId, CancellationToken cancellationToken)
        {
           var profile=await _expertRepository.GetProfile(appuserId, cancellationToken);
            if (profile == null)
            {
                _logger.LogWarning("کارشناس با کد {ExpertId} در سیستم یافت نشد.", appuserId);
                return Result<ProfileExpertDto>.Failure("متخصص مورد نظر یافت نشد.");
            }
            _logger.LogInformation("اطلاعات پروفایل کارشناس {ExpertId} با موفقیت بارگذاری شد.", appuserId);
            return Result<ProfileExpertDto>.Success(profile);
        }

        public async Task<Result<bool>> ChangeProfile(int appuserId, ProfileExpertDto profileExpertDto, bool isAdmin, CancellationToken cancellationToken)
        {
           
            _logger.LogInformation("شروع فرآیند ویرایش پروفایل کارشناس با کد کاربری {Id}. توسط: {Role}", appuserId, isAdmin ? "Admin" : "Expert");

   
           var isCityValid = await _cityRepository.IsExist(profileExpertDto.CityId, cancellationToken);
            if (!isCityValid)
            {
                _logger.LogWarning("شهر با کد {CityId} معتبر نیست.", profileExpertDto.CityId);
                return Result<bool>.Failure("شهر انتخاب شده در سیستم یافت نشد.");
            }

            
           if (isAdmin && !string.IsNullOrEmpty(profileExpertDto.Email)) 
           {
                var user = await _userManager.FindByIdAsync(appuserId.ToString());
                if (user != null && user.Email != profileExpertDto.Email)
                {
                    
                    user.Email = profileExpertDto.Email;
                    user.NormalizedEmail = profileExpertDto.Email.ToUpper();
                    user.UserName = profileExpertDto.Email;
                    user.NormalizedUserName = profileExpertDto.Email.ToUpper();
                    user.EmailConfirmed = true;

                    var identityResult = await _userManager.UpdateAsync(user);
                    if (!identityResult.Succeeded)
                    {
                         _logger.LogError("خطا در بروزرسانی ایمیل کارشناس در Identity."); 
                         return Result<bool>.Failure("بروزرسانی ایمیل توسط سیستم Identity با شکست مواجه شد.");
                    }
                }
           }

       
            var isUpdated = await _expertRepository.ChangeProfile(appuserId, profileExpertDto, isAdmin, cancellationToken);

            if (!isUpdated)
            {
                 _logger.LogError("بروزرسانی اطلاعات کارشناس {Id} در دیتابیس انجام نشد.", appuserId); 
                  return Result<bool>.Failure("ذخیره تغییرات در پایگاه داده با خطا مواجه شد.");
            }

           
             _logger.LogInformation("پروفایل کارشناس {Id} با موفقیت ویرایش گردید.", appuserId); 
              return Result<bool>.Success(true, "اطلاعات کارشناس با موفقیت بروزرسانی شد.");
        }

        public async Task<Result<ExpertPagedResultDto>> GetAll( int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var experts = await _expertRepository.GetAll( pageNumber, pageSize, cancellationToken);


            if (experts == null || !experts.Experts.Any())
            {
                return Result<ExpertPagedResultDto>.Failure("هیچ کارشناسی با مشخصات وارد شده یافت نشد.");
            }

            return Result<ExpertPagedResultDto>.Success(experts);
        }
        
            public async Task<Result<bool>> Delete(int appUserId, CancellationToken cancellationToken)
            {
               
                var identityResult = await _accountService.DeleteUserIdentity(appUserId, cancellationToken);
                if (!identityResult.IsSuccess) return identityResult;

                
                var repoResult = await _expertRepository.Delete(appUserId, cancellationToken);

                if (!repoResult)
                    return Result<bool>.Failure("بخش هویت حذف شد اما رکورد کارشناس یافت نشد یا عملیات ناقص ماند.");

                return Result<bool>.Success(true, "کارشناس با موفقیت حذف گردید.");
            }
        

        public async Task<int> GetIdByAppUserId(int appUserId, CancellationToken cancellationToken)
        {
            return await _expertRepository.GetIdByAppUserId(appUserId, cancellationToken);
        }

     
    }
}

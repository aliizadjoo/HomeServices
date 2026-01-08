using App.Domain.Core._common;
using App.Domain.Core.Contract.AccountAgg.Services;
using App.Domain.Core.Contract.AdminAgg.Repository;
using App.Domain.Core.Contract.AdminAgg.Service;
using App.Domain.Core.Dtos.AdminAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.AdminAgg
{
    public class AdminService(IAdminRepository _adminRepository
        , IAccountService _accountService, ILogger<AdminService> _logger 
        , UserManager<AppUser> _userManager)
        : IAdminService
    {
        public async Task<Result<AdminPagedResultDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var admins = await _adminRepository.GetAll(pageNumber, pageSize, cancellationToken);


            if (admins == null || !admins.Admins.Any())
            {
                return Result<AdminPagedResultDto>.Failure("هیچ ادمینی با مشخصات وارد شده یافت نشد.");
            }

            return Result<AdminPagedResultDto>.Success(admins);
        }


        public async Task<Result<bool>> Create(int userId, CancellationToken cancellationToken)
        {

            string generatedStaffCode = "ADM-" + new Random().Next(10000, 99999).ToString();


            CreateAdminDto createAdminDto = new CreateAdminDto()
            {
                AppUserId = userId,
                StaffCode = generatedStaffCode,

            };

            var result = await _adminRepository.Create(createAdminDto, cancellationToken);


            if (result <= 0)
            {
                _logger.LogError("خطا در ایجاد پروفایل ادمین برای کاربر با شناسه {UserId}", userId);
                return Result<bool>.Failure("خطا در ایجاد پروفایل مدیر");
            }

            _logger.LogInformation("پروفایل مدیر با کد پرسنلی {StaffCode} ایجاد شد", generatedStaffCode);
            return Result<bool>.Success(true);
        }

        public async Task<Result<AdminProfileDto>> GetProfileByAppUserId(int appUserId, CancellationToken cancellationToken)
        {

            var admin = await _adminRepository.GetByAppUserId(appUserId, cancellationToken);


            if (admin == null)
            {
                return Result<AdminProfileDto>.Failure("مدیری با این مشخصات یافت نشد.");
            }


            var adminProfileDto = new AdminProfileDto
            {
                AppUserId = admin.AppUserId,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                ImagePath = admin.ImagePath,
                StaffCode = admin.StaffCode,
                TotalRevenue = admin.TotalRevenue,
                PhoneNumber = admin.PhoneNumber,
            };

            return Result<AdminProfileDto>.Success(adminProfileDto);
        }




        public async Task<Result<bool>> UpdateProfile(AdminProfileDto adminProfileDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("شروع ویرایش پروفایل مدیر با شناسه {Id}", adminProfileDto.AppUserId);

           
            var user = await _userManager.FindByIdAsync(adminProfileDto.AppUserId.ToString());
            if (user == null) return Result<bool>.Failure("کاربر یافت نشد.");

            if (user.Email != adminProfileDto.Email)
            {
                user.Email = adminProfileDto.Email;
                user.UserName = adminProfileDto.Email;
                user.NormalizedEmail = adminProfileDto.Email!.ToUpper();
                user.NormalizedUserName = adminProfileDto.Email.ToUpper();
                user.EmailConfirmed = true;


                var identityResult = await _userManager.UpdateAsync(user);

              
                if (!identityResult.Succeeded)
                {
                    return Result<bool>.Failure("بروزرسانی ایمیل در سیستم Identity با شکست مواجه شد.");
                }
            }

        
            var isUpdated = await _adminRepository.Update(adminProfileDto, cancellationToken);

            if (!isUpdated)
            {
                _logger.LogWarning("تغییری برای مدیر {Id} ثبت نشد.", adminProfileDto.AppUserId);
                return Result<bool>.Failure("بروزرسانی پروفایل انجام نشد.");
            }

            _logger.LogInformation("پروفایل مدیر {Id} با موفقیت بروزرسانی شد.", adminProfileDto.AppUserId);
            return Result<bool>.Success(true, "ویرایش با موفقیت انجام شد.");
        }


        public async Task<Result<bool>> Delete(int appUserId, CancellationToken cancellationToken)
        {
            var identityResult = await _accountService.DeleteUserIdentity(appUserId, cancellationToken);
            if (!identityResult.IsSuccess) return identityResult;

            var repoResult = await _adminRepository.Delete(appUserId, cancellationToken);

            return repoResult
                ? Result<bool>.Success(true, "مدیر با موفقیت حذف شد.")
                : Result<bool>.Failure("خطا در حذف رکورد مدیریت.");
        }

    }
}

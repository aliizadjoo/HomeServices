using App.Domain.Core._common;
using App.Domain.Core.Contract.AdminAgg.Repository;
using App.Domain.Core.Contract.AdminAgg.Service;
using App.Domain.Core.Dtos.AdminAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.AdminAgg
{
    public class AdminService(IAdminRepository _adminRepository  , ILogger<AdminService> _logger) : IAdminService
    {
        public async Task<Result<AdminPagedResultDto>> GetAll( int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var admins = await _adminRepository.GetAll( pageNumber, pageSize, cancellationToken);


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
    }
}

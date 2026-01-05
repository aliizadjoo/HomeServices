using App.Domain.Core._common;
using App.Domain.Core.Contract.AccountAgg.Services;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.AccountAgg
{
    public class AccountService(UserManager<AppUser> _userManager) : IAccountService
    {
        public async Task<Result<bool>> DeleteUserIdentity(int appUserId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(appUserId.ToString());
            if (user == null || user.IsDeleted)
                return Result<bool>.Failure("کاربر یافت نشد یا قبلاً حذف شده است.");

            string suffix = $"_del_{DateTime.Now.Ticks}";

          
            user.Email += suffix;
            user.UserName += suffix;

            
            user.NormalizedEmail += suffix.ToUpper();
            user.NormalizedUserName += suffix.ToUpper();

            user.IsDeleted = true;

           
            var identityResult = await _userManager.UpdateAsync(user);

            if (!identityResult.Succeeded)
                return Result<bool>.Failure("خطا در به‌روزرسانی اطلاعات هویت کاربر در سیستم Identity.");

            return Result<bool>.Success(true);
        }
    }
}

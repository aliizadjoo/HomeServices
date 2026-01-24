using App.Domain.Core._common;
using App.Domain.Core.Constants;
using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Contract.AdminAgg.Service;
using App.Domain.Core.Contract.CustomerAgg.AppService;
using App.Domain.Core.Contract.CustomerAgg.Service;
using App.Domain.Core.Contract.ExpertAgg.Service;
using App.Domain.Core.Dtos.AccountAgg;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using App.Domain.Core.Dtos.UserAgg;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.AccountAgg
{
    public class AccountAppService
        (UserManager<AppUser> _userManager,
          SignInManager<AppUser> _signInManager,
          ICustomerService _customerService,
            IExpertService _expertService,
            IAdminService _adminService,
          ILogger<AccountAppService> _logger,
          RoleManager<IdentityRole<int>> _roleManager
        ) : IAccountAppService
    {



        public async Task<List<RoleDto>> GetRoles(CancellationToken cancellationToken)
        {
           
            return await _roleManager.Roles
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name!
                }).ToListAsync(cancellationToken);
        }

        public async Task<IdentityResult> Register(UserRegisterDto userRegisterDto  ,CancellationToken cancellationToken)
        {
         
            var user = new AppUser
            {
                UserName = userRegisterDto.Email,
                Email = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                ImagePath = userRegisterDto.ImagePath,
                
            };

            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);

            if (result.Succeeded)
            {
              
                var roleResult = await _userManager.AddToRoleAsync(user, userRegisterDto.Role);

                if (roleResult.Succeeded)
                {

                    if (userRegisterDto.Role == "Expert")
                    {
                        var profileResult = await _expertService.Create(user.Id, userRegisterDto.CityId, cancellationToken);

                        if (!profileResult.IsSuccess)
                        {
                           
                            await _userManager.DeleteAsync(user);

                            
                            _logger.LogError("خطا در ساخت پروفایل کارشناس: {Message}", profileResult.Message);

                           
                            return IdentityResult.Failed(new IdentityError { Description = profileResult.Message });
                        }
                    }
                    else if (userRegisterDto.Role == "Customer")
                    {
                        var profileResult = await _customerService.Create(user.Id, userRegisterDto.CityId, cancellationToken);

                        if (!profileResult.IsSuccess)
                        {
                            await _userManager.DeleteAsync(user);
                            _logger.LogError("خطا در ساخت پروفایل مشتری: {Message}", profileResult.Message);
                            return IdentityResult.Failed(new IdentityError { Description = profileResult.Message });
                        }
                    }

                    else if (userRegisterDto.Role == "Admin") 
                    {

                        var profileResult = await _adminService.Create(user.Id, cancellationToken);
                        if (!profileResult.IsSuccess)
                        {
                            await _userManager.DeleteAsync(user);
                            _logger.LogError("خطا در ساخت پروفایل ادمین: {Message}", profileResult.Message);
                            return IdentityResult.Failed(new IdentityError { Description = profileResult.Message });
                        }
                    }

                    else
                    {
                       
                        await _userManager.DeleteAsync(user);
                        _logger.LogError("نقش نامعتبر است: {Role}", userRegisterDto.Role);
                        return IdentityResult.Failed(new IdentityError { Description = "نقش انتخاب شده معتبر نیست." });
                    }
                }
                else
                {
                    await _userManager.DeleteAsync(user);
                    _logger.LogError("نقش اختصاص نیافت: {Errors}", string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    return IdentityResult.Failed(new IdentityError { Description = "خطا در اختصاص نقش به کاربر." });
                }
            }
            else
            {

                _logger.LogWarning("ثبت نام کاربر شکست خورد: {Email}", userRegisterDto.Email);
            }

            return result;
        }

        public async Task<bool> Login(UserLoginDto userLoginDto)
        {

            var result = await _signInManager.PasswordSignInAsync(userLoginDto.UserName, userLoginDto.Password, userLoginDto.RememberMe, lockoutOnFailure: false);


            if (result.Succeeded)
            {
                _logger.LogInformation("Login successful for user: {UserName}", userLoginDto.UserName);
                return true;
            }
            _logger.LogWarning("Login failed for user: {UserName}. Result: {Result}", userLoginDto.UserName, result.ToString());

            return false;
        }

        public async Task Logout()
        {

            _logger.LogInformation("User is logging out.");
            await _signInManager.SignOutAsync();
        }

        public int GetUserId(ClaimsPrincipal user)
        {
            
            var userId = _userManager.GetUserId(user);
            return userId != null ? int.Parse(userId) : 0;
        }

        public int GetCustomerId(ClaimsPrincipal user)
        {
           
            var claim = user.FindFirst(CustomClaimTypes.CustomerId);
            return claim != null ? int.Parse(claim.Value) : 0;
        }

        public int GetExpertId(ClaimsPrincipal user)
        {
            var claim = user.FindFirst(CustomClaimTypes.ExpertId);
            return claim != null ? int.Parse(claim.Value) : 0;
        }

        public async Task<Result<bool>> ChangePassword(ClaimsPrincipal userPrincipal, ChangePasswordDto changePasswordDto)
        {
           
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                return Result<bool>.Failure("کاربر یافت نشد.");
            }

            
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            if (result.Succeeded)
            {
                _logger.LogInformation("کاربر {Email} رمز عبور خود را تغییر داد.", user.Email);
                return Result<bool>.Success(true, "رمز عبور با موفقیت تغییر یافت.");
            }

          
            var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<bool>.Failure(errorMessages);
        }


        public async Task<Result<bool>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
          
            var user = await _userManager.FindByIdAsync(changePasswordDto.AppUserId.ToString());

            if (user == null)
            {
                return Result<bool>.Failure("کاربر یافت نشد.");
            }

            
            var result = await _userManager.ChangePasswordAsync(
                user,
                changePasswordDto.OldPassword,
                changePasswordDto.NewPassword
            );

            if (result.Succeeded)
            {
                _logger.LogInformation("کاربر با شناسه {UserId} رمز عبور خود را تغییر داد.", changePasswordDto.AppUserId);
                return Result<bool>.Success(true, "رمز عبور با موفقیت تغییر یافت.");
            }

            var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<bool>.Failure(errorMessages);

        }

    }
}

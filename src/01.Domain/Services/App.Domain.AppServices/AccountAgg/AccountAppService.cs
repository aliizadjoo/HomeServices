using App.Domain.Core._common;
using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Dtos.AccountAgg;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
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
          ILogger<AccountAppService> _logger
        ) : IAccountAppService
    {
        public async Task<IdentityResult> Register(UserRegisterDto userRegisterDto)
        {
            var user = new AppUser
            {
                UserName = userRegisterDto.Email,
                Email = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName

            };


            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);


            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, userRegisterDto.Role);

                if (!roleResult.Succeeded)
                {

                    _logger.LogError("User {Email} created but failed to assign role {Role}. Errors: {Errors}",
                        user.Email, userRegisterDto.Role, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                _logger.LogWarning("Identity user creation failed for {Email}.", userRegisterDto.Email);
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
    }
}

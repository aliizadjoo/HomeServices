using App.Domain.Core._common;
using App.Domain.Core.Dtos.AccountAgg;
using App.Domain.Core.Dtos.UserAgg;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.AccountAgg.AppServices
{
    public interface IAccountAppService
    {
        public Task<IdentityResult> Register(UserRegisterDto userRegisterDto, CancellationToken cancellationToken);

        public  Task<List<RoleDto>> GetRoles(CancellationToken cancellationToken);

        public Task<bool> Login(UserLoginDto command);


        public Task Logout();

        public int GetUserId(ClaimsPrincipal user);

        public Task<Result<bool>> ChangePassword(ClaimsPrincipal userPrincipal, ChangePasswordDto changePasswordDto);
    }
}

using App.Domain.Core.Dtos.AccountAgg;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.AccountAgg.AppServices
{
    public interface IAccountAppService
    {
        Task<IdentityResult> Register(UserRegisterDto command);

        
        Task<bool> Login(UserLoginDto command);

    
        Task Logout();
    }
}

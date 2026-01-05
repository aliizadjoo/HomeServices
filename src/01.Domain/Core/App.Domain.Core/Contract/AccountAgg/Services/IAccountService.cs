using App.Domain.Core._common;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.AccountAgg.Services
{
    public interface IAccountService
    {

        public Task<Result<bool>> DeleteUserIdentity(int appUserId, CancellationToken cancellationToken);


    }
}


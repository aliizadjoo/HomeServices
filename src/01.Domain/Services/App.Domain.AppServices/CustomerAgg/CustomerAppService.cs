using App.Domain.Core._common;
using App.Domain.Core.Contract.CustomerAgg.AppService;
using App.Domain.Core.Contract.CustomerAgg.Service;
using App.Domain.Core.Dtos.CustomerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.CustomerAgg
{
    public class CustomerAppService(ICustomerService _customerService) : ICustomerAppService
    {
        public async Task<Result<bool>> ChangeProfileCustomer(int appuserId, ProfileCustomerDto profileCustomerDto, CancellationToken cancellationToken)
        {
           return await _customerService.ChangeProfileCustomer(appuserId, profileCustomerDto, cancellationToken);
        }

        public async Task<Result<ProfileCustomerDto>> GetProfileCustomer(int appuserId, CancellationToken cancellationToken)
        {
            return await _customerService.GetProfileCustomer(appuserId, cancellationToken);
        }
    }
}

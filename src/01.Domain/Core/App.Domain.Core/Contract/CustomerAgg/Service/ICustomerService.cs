using App.Domain.Core._common;
using App.Domain.Core.Dtos.CustomerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CustomerAgg.Service
{
    public interface ICustomerService
    {
        public Task<Result<ProfileCustomerDto>> GetProfileCustomerByAppUserId(int customerId, CancellationToken cancellationToken);

        public Task<Result<bool>> ChangeProfileCustomer(int appuserId, ProfileCustomerDto profileCustomerDto, bool isAdmin, CancellationToken cancellationToken);

        public Task<Result<bool>> Create(int userId, int cityId, CancellationToken cancellationToken);
        public Task<Result<CustomerPagedResultDto>> GetAll( int pageNumber, int pageSize, CancellationToken cancellationToken);


        public Task<Result<bool>> DeleteUser(int appUserId, CancellationToken cancellationToken);



    }
}

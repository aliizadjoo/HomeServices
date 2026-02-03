using App.Domain.Core._common;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CustomerAgg.Repository
{
    public interface ICustomerRepository
    {
        public Task<ProfileCustomerDto?> GetProfileCustomer(int customerId, CancellationToken cancellationToken);
        public Task<bool> ChangeProfileCustomer(int customerId, ProfileCustomerDto profileCustomerDto,  CancellationToken cancellationToken);
        public Task<int> Create(CreateCustomerDto custmoerDto, CancellationToken cancellationToken);

        public Task<CustomerPagedResultDto> GetAll( int pageNumber, int pageSize, CancellationToken cancellationToken);

        public Task<bool> Delete(int appUserId,  CancellationToken cancellationToken);
        public Task<int> GetIdByAppUserId(int appUserId, CancellationToken cancellationToken);

        public Task<decimal?> GetBalance(int customerId  , CancellationToken cancellationToken);

        public Task<Customer?> GetById(int customerId, CancellationToken cancellationToken);


    }
}

using App.Domain.Core.Dtos.CustomerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CustomerAgg.Repository
{
    public interface ICustomerRepository
    {
        public Task<ProfileCustomerDto?> GetProfileCustomer(int customerId , CancellationToken cancellationToken);
        public Task<bool> ChangeProfileCustomer(int customerId , ProfileCustomerDto profileCustomerDto, CancellationToken cancellationToken);
    }
}

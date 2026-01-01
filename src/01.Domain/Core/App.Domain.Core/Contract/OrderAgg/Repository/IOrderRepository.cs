using App.Domain.Core.Dtos.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.OrderAgg.Repository
{
    public interface IOrderRepository
    {
        public Task<List<OrderDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);

        public  Task<int> GetCount(CancellationToken cancellationToken);
    }
}

using App.Domain.Core._common;
using App.Domain.Core.Contract.OrderAgg.Repository;
using App.Domain.Core.Dtos.OrderAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.OrderAgg.Service
{
    public interface IOrderService
    {
        public Task<Result<List<OrderDto>>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
        public Task<int> GetCount(CancellationToken cancellationToken);
       

    }
}

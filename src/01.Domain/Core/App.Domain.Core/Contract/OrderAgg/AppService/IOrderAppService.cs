using App.Domain.Core._common;
using App.Domain.Core.Contract.OrderAgg.Repository;
using App.Domain.Core.Dtos.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.OrderAgg.AppService
{
    public interface IOrderAppService
    {
        public Task<Result<OrderPagedDtos>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);

        public Task<Result<bool>> Create(OrderCreateDto orderCreateDto, CancellationToken cancellationToken);
        public Task<Result<OrderPagedDtos>> GetOrdersByAppUserId(int appUserId, int pageNumber, int pageSize, CancellationToken cancellationToken);
        public Task<Result<AvailableOrdersPagedDto>> GetAvailableForExpert(int expertId, int pageNumber, int pageSize, CancellationToken cancellationToken);
        public Task<bool> IsFinished(int orderId, CancellationToken cancellationToken);
        public Task<Result<OrderSummaryDto>> GetOrderSummary(int orderId, CancellationToken cancellationToken);
        public  Task<Result<bool>> Pay(int orderId, int customerId, CancellationToken cancellationToken);
    }
}

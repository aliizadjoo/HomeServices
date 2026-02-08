using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Entities;
using App.Domain.Core.Enums.OrderAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.OrderAgg.Repository
{
    public interface IOrderRepository
    {
        public Task<OrderPagedDtos> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
        public Task<int> Create(OrderCreateDto orderCreateDto , CancellationToken cancellationToken);
        public Task<int> ChangeStatus(int orderId, OrderStatus newStatus , CancellationToken cancellationToken);
        public Task<OrderPagedDtos> GetOrdersByAppUserId(int appUserId,int pageNumber, int pageSize, CancellationToken cancellationToken);


        public Task<AvailableOrdersPagedDto> GetAvailableForExpertAsync(int expertId, int pageNumber, int pageSize, CancellationToken cancellationToken);

        public Task<OrderSummaryDto?> GetOrderDetails(int orderId , CancellationToken cancellationToken);

        public Task<bool> IsExists(int orderId, CancellationToken cancellationToken);

        public Task<OrderStatus> GetStatus(int orderId, CancellationToken cancellationToken);
        Task<bool> IsPaid(int orderId, CancellationToken cancellationToken);

        public Task<int> SaveChanges(CancellationToken cancellationToken);

        public Task<Order?> GetById(int orderId , CancellationToken cancellationToken);


        public  Task<bool> IsOrderBelongToCustomer(int orderId, int customerId, CancellationToken cancellationToken);



    }
}

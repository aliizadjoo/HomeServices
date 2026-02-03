using App.Domain.Core._common;
using App.Domain.Core.Contract.OrderAgg.AppService;
using App.Domain.Core.Contract.OrderAgg.Service;
using App.Domain.Core.Dtos.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.OrderAgg
{
    public class OrderAppService(IOrderService _orderService) : IOrderAppService
    {
        public async Task<Result<bool>> Create(OrderCreateDto orderCreateDto, CancellationToken cancellationToken)
        {
           return await _orderService.Create(orderCreateDto, cancellationToken);
        }

        public async Task<Result<OrderPagedDtos>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _orderService.GetAll(pageNumber, pageSize, cancellationToken);
        }

        public async Task<Result<AvailableOrdersPagedDto>> GetAvailableForExpert(int expertId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
          return await _orderService.GetAvailableForExpert(expertId, pageNumber, pageSize, cancellationToken);
        }

        public async Task<Result<OrderPagedDtos>> GetOrdersByAppUserId(int appUserId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
          return await  _orderService.GetOrdersByAppUserId(appUserId, pageNumber, pageSize, cancellationToken);
        }

        public async Task<Result<OrderSummaryDto>> GetOrderSummary(int orderId, CancellationToken cancellationToken)
        {
          return await   _orderService.GetOrderSummary(orderId, cancellationToken);
        }

        public async Task<bool> IsFinished(int orderId, CancellationToken cancellationToken)
        {
          return await _orderService.IsFinished(orderId, cancellationToken);
        }

        //public async Task<> Pay(int orderId , int price , CancellationToken cancellationToken) 
        //{
           
        //}
    }
}

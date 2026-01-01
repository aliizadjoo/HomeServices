using App.Domain.Core._common;
using App.Domain.Core.Contract.OrderAgg.Repository;
using App.Domain.Core.Contract.OrderAgg.Service;
using App.Domain.Core.Dtos.OrderAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.OrderAgg
{
    public class OrderService(IOrderRepository _orderRepository , ILogger<OrderService> _logger) : IOrderService
    {
        public async Task<Result<OrderPagedDtos>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            _logger.LogInformation("درخواست دریافت لیست سفارشات (صفحه: {PageNumber}، تعداد در صفحه: {PageSize})",
                pageNumber, pageSize);

            var orders = await _orderRepository.GetAll(pageNumber, pageSize, cancellationToken);

            if (orders == null || !orders.orderDtos.Any())
            {
                _logger.LogWarning("در صفحه {PageNumber} هیچ سفارشی یافت نشد.", pageNumber);
                return Result<OrderPagedDtos>.Failure("سفارشی یافت نشد.");
            }

            _logger.LogInformation("تعداد {OrderCount} سفارش با موفقیت برای صفحه {PageNumber} بازیابی شد.",
                orders.TotalCount, pageNumber);

            return Result<OrderPagedDtos>.Success(orders);
        }


    }
}

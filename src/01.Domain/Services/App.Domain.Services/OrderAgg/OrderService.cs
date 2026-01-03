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
        public async Task<Result<bool>> Create(OrderCreateDto orderCreateDto, CancellationToken cancellationToken)
        {
          
            var orderId = await _orderRepository.Create(orderCreateDto, cancellationToken);

           
            if (orderId > 0)
            {
               
                return Result<bool>.Success(true, "سفارش شما با موفقیت ثبت شد و در انتظار پیشنهاد متخصصین است.");
            }

           
            return Result<bool>.Failure("متأسفانه در فرآیند ثبت سفارش خطایی رخ داده است. لطفاً دوباره تلاش کنید.");
        }

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
        public async Task<Result<OrderPagedDtos>> GetOrdersByAppUserId(int appUserId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            
            _logger.LogInformation("Request received to fetch orders for AppUserId: {AppUserId}. Page: {PageNumber}, Size: {PageSize}",
                appUserId, pageNumber, pageSize);

         
            var result = await _orderRepository.GetOrdersByAppUserId(appUserId, pageNumber, pageSize, cancellationToken);

           
            if (result == null || !result.orderDtos.Any())
            {
               
                _logger.LogWarning("No orders found in the database for AppUserId: {AppUserId}", appUserId);

                return Result<OrderPagedDtos>.Failure("شما هنوز هیچ سفارشی ثبت نکرده‌اید.");
            }

          
            _logger.LogInformation("Successfully retrieved {Count} orders for AppUserId: {AppUserId}. Total Count: {TotalCount}",
                result.orderDtos.Count, appUserId, result.TotalCount);

            
            return Result<OrderPagedDtos>.Success(result);
        }

    }
}

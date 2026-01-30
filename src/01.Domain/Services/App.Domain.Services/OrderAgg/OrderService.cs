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

            var executionDateTime = orderCreateDto.ExecutionDate.Date.Add(orderCreateDto.ExecutionTime);

            
            if (executionDateTime < DateTime.Now)
            {
                return Result<bool>.Failure("زمان انتخاب شده نمی‌تواند در گذشته باشد. لطفاً ساعت و تاریخ معتبری را انتخاب کنید.");
            }

            var orderId = await _orderRepository.Create(orderCreateDto, cancellationToken);

           
            if (orderId > 0)
            {
               
                return Result<bool>.Success(true, "سفارش شما با موفقیت ثبت شد و در انتظار تایید ادمین  است.");
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


        public async Task<Result<AvailableOrdersPagedDto>> GetAvailableForExpert(int expertId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to fetch available orders for Expert (AppUserId: {AppUserId}). Page: {Page}, Size: {Size}",
                expertId, pageNumber, pageSize);

            var result = await _orderRepository.GetAvailableForExpertAsync(expertId, pageNumber, pageSize, cancellationToken);

           
            if (result == null || result.TotalCount == 0)
            {
                _logger.LogWarning("No available orders matching skills and city were found for Expert: {ExpertId}", expertId);

                return Result<AvailableOrdersPagedDto>.Failure("در حال حاضر سفارش جدیدی متناسب با تخصص و شهر شما ثبت نشده است.");
            }

            _logger.LogInformation("Successfully retrieved {Count} available orders for Expert: {AppUserId}",
                result.AvailableOrdersDto.Count, expertId);

            return Result<AvailableOrdersPagedDto>.Success(result);
        }

        public async Task<Result<OrderSummaryDto>> GetOrderSummary(int orderId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching order summary for OrderId: {OrderId}", orderId);

            var orderSummary = await _orderRepository.GetOrderDetails(orderId, cancellationToken);

            if (orderSummary == null)
            {
                _logger.LogWarning("Order summary not found for OrderId: {OrderId}", orderId);
                return Result<OrderSummaryDto>.Failure("مشخصات سفارش مورد نظر یافت نشد یا ممکن است لغو شده باشد.");
            }

            return Result<OrderSummaryDto>.Success(orderSummary);
        }
    }
}

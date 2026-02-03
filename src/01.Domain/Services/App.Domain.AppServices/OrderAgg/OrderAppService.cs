using App.Domain.Core._common;
using App.Domain.Core.Contract.CustomerAgg.Service;
using App.Domain.Core.Contract.ExpertAgg.Service;
using App.Domain.Core.Contract.OrderAgg.AppService;
using App.Domain.Core.Contract.OrderAgg.Service;
using App.Domain.Core.Contract.ProposalAgg.Service;
using App.Domain.Core.Dtos.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.OrderAgg
{
    public class OrderAppService
        (IOrderService _orderService 
        , IProposalService _proposalService 
        , ICustomerService _customerService
        ,IExpertService _expertService) 
        : IOrderAppService
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

        public async Task<Result<bool>> Pay(int orderId, int customerId, CancellationToken cancellationToken)
        {

            var resultPaymentStatus = await _orderService.CheckIsPaid(orderId, cancellationToken);

            if (resultPaymentStatus.IsSuccess)
            {
                return Result<bool>.Failure(resultPaymentStatus.Message);
            }

            var priceResult = await _proposalService.GetPriceByOrderId(orderId, cancellationToken);
            if (!priceResult.IsSuccess)
                return Result<bool>.Failure(priceResult.Message);

           
            var expertIdResult = await _proposalService.GetExpertIdByOrderId(orderId, cancellationToken);
            if (!expertIdResult.IsSuccess)
                return Result<bool>.Failure(expertIdResult.Message);

           
            var deductResult = await _customerService
                .DeductBalance(customerId, priceResult.Data, cancellationToken);

            if (!deductResult.IsSuccess)
                return Result<bool>.Failure(deductResult.Message);

            
            var addResult = await _expertService
                .AddBalance(expertIdResult.Data, priceResult.Data, cancellationToken);

            if (!addResult.IsSuccess)
                return Result<bool>.Failure(addResult.Message);

           
            await _orderService.MarkAsPaid(orderId, cancellationToken);

         
            var resultSave= await _orderService.SaveChanges(cancellationToken);
            if (!resultSave.IsSuccess)
            {
                return Result<bool>.Failure(resultSave.Message);
            }

            return Result<bool>.Success(true, "پرداخت با موفقیت انجام شد.");
        }

    }
}

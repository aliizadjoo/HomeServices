using App.Domain.Core._common;
using App.Domain.Core.Contract.OrderAgg.Service;
using App.Domain.Core.Contract.ProposalAgg.Service;
using App.Domain.Core.Contract.ReviewAgg.AppService;
using App.Domain.Core.Contract.ReviewAgg.Service;
using App.Domain.Core.Dtos.ReviewAgg;
using App.Domain.Core.Enums.ReviewAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.ReviewAgg
{
    public class ReviewAppService
        (IReviewService _reviewService 
        ,IOrderService _orderService
        , IProposalService _proposalService

        )
        : IReviewAppService
    {
        public async Task<Result<bool>> ChangeStatus(int id, ReviewStatus status, CancellationToken cancellationToken)
        {
          return await _reviewService.ChangeStatus(id, status, cancellationToken);
        }

        public async Task<Result<bool>> Create(
                  CreateReviewDto dto,
               CancellationToken cancellationToken)
        {
           
            var isFinished = await _orderService
                .IsFinished(dto.OrderId, cancellationToken);

            if (!isFinished)
            {
                return Result<bool>.Failure(
                    "تا زمانی که سفارش تکمیل نشده است امکان ثبت نظر وجود ندارد."
                );
            }

            var hasCommented = await _reviewService
                .HasCustomerCommentedOnOrder(dto.OrderId, dto.CustomerId, cancellationToken);

            if (hasCommented.Data)
            {
                return Result<bool>.Failure(
                    "شما قبلاً برای این سفارش نظر ثبت کرده‌اید."
                );
            }

          
            var expertResult = await _proposalService
                .GetExpertIdByOrderId(dto.OrderId, cancellationToken);

            if (!expertResult.IsSuccess)
            {
                return Result<bool>.Failure(expertResult.Message);
            }

            dto.ExpertId = expertResult.Data;

           
            return await _reviewService.Create(dto, cancellationToken);
        }

        public async Task<Result<ReviewPagedDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _reviewService.GetAll(pageNumber, pageSize, cancellationToken);
        }

        public async Task<Result<ReviewPagedDto>> GetByExpertId(int pageSize, int pageNumber, int expertId, CancellationToken cancellationToken)
        {
            return await _reviewService.GetByExpertId(pageSize , pageNumber , expertId, cancellationToken);
        }

        public async Task<Result<bool>> HasCustomerCommentedOnOrder(int orderId, int customerId, CancellationToken cancellationToken) 
        {
          return await  _reviewService.HasCustomerCommentedOnOrder(orderId, customerId, cancellationToken);
        }
    }
}

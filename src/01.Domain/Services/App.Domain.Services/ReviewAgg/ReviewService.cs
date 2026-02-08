using App.Domain.Core._common;
using App.Domain.Core.Contract.ExpertAgg.Repository;
using App.Domain.Core.Contract.ReviewAgg.Repository;
using App.Domain.Core.Contract.ReviewAgg.Service;
using App.Domain.Core.Dtos.ReviewAgg;
using App.Domain.Core.Enums.ReviewAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.ReviewAgg
{
    public class ReviewService(IReviewRepository _reviewRepository , IExpertRepository _expertRepository) : IReviewService
    {
        public async Task<Result<ReviewPagedDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetAll(pageNumber, pageSize, cancellationToken);

            if (reviews == null || !reviews.ReviewDtos.Any())
            {
                return Result<ReviewPagedDto>.Failure("هیچ نظری در سیستم ثبت نشده است.");
            }

            return Result<ReviewPagedDto>.Success(reviews);
        }

      
        public async Task<Result<bool>> ChangeStatus(int id, ReviewStatus status, CancellationToken cancellationToken)
        {
            
            var expertId = await _reviewRepository.GetExpertIdByReviewId(id, cancellationToken);

            if (expertId <= 0)
            {
                return Result<bool>.Failure("نظری با این مشخصات یافت نشد.");
            }

            var isChanged = await _reviewRepository.ChangeStatus(id, status, cancellationToken);

            if (!isChanged)
            {
                return Result<bool>.Failure("عملیات تغییر وضعیت نظر با شکست مواجه شد.");
            }

            var newAverageScore = await _reviewRepository.AverageScore(expertId, cancellationToken);

            bool isUpdated = await _expertRepository.UpdateExpertScore(expertId, newAverageScore, cancellationToken);

            if (isUpdated)
            {
                return Result<bool>.Success(true, "وضعیت نظر تغییر و امتیاز میانگین کارشناس با موفقیت بروزرسانی شد.");
            }

            return Result<bool>.Failure("وضعیت نظر تغییر کرد، اما خطایی در بروزرسانی امتیاز نهایی کارشناس رخ داد.");
        }
        public async Task<Result<ReviewPagedDto>> GetByExpertId(int pageSize, int pageNumber, int expertId, CancellationToken cancellationToken)
        {
            var reviews  = await _reviewRepository.GetByExpertId(pageSize , pageNumber , expertId, cancellationToken);
            if (reviews==null || !reviews.ReviewDtos.Any())
            {
                return Result<ReviewPagedDto>.Failure("هیچ نظری برای این کارشناس در سیستم یافت نشد.");
            }

            return Result<ReviewPagedDto>.Success(reviews, "عملیات با موفقیت انجام شد .");
        }

        public async Task<Result<bool>> Create(CreateReviewDto createReviewDto, CancellationToken cancellationToken)
        {
            var result=await _reviewRepository.Create(createReviewDto, cancellationToken);
            if (result>0)
            {
                return Result<bool>.Success(true, "نظر شما با موفقیت ثبت شد .");
            }

            return Result<bool>.Failure("متأسفانه در فرآیند ثبت نظر خطایی رخ داده است. لطفاً دوباره تلاش کنید.");
        }

        public async Task<Result<bool>> HasCustomerCommentedOnOrder(int orderId, int customerId, CancellationToken cancellationToken)
        {
            var result=await _reviewRepository.HasCustomerCommentedOnOrder(orderId, customerId, cancellationToken);
            if (result)
            {
                return Result<bool>.Success(true , "شما قبلا برای این سفارش نظر ثبت کرده اید .");
            }

            return Result<bool>.Failure("نظری برای این سفارش ثبت نشده است . .");
        }
    }
}

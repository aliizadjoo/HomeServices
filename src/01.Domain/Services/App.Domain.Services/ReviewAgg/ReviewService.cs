using App.Domain.Core._common;
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
    public class ReviewService(IReviewRepository _reviewRepository) : IReviewService
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
            var isChanged = await _reviewRepository.ChangeStatus(id, status, cancellationToken);

            if (isChanged)
            {
                return Result<bool>.Success(true, "وضعیت نظر با موفقیت تغییر یافت.");
            }

            return Result<bool>.Failure("تغییر وضعیت انجام نشد.");
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
    }
}

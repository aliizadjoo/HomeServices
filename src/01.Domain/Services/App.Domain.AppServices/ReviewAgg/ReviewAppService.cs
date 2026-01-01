using App.Domain.Core._common;
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
    public class ReviewAppService(IReviewService _reviewService) : IReviewAppService
    {
        public async Task<Result<bool>> ChangeStatus(int id, ReviewStatus status, CancellationToken cancellationToken)
        {
          return await _reviewService.ChangeStatus(id, status, cancellationToken);
        }

        public async Task<Result<ReviewPagedDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _reviewService.GetAll(pageNumber, pageSize, cancellationToken);
        }

       
    }
}

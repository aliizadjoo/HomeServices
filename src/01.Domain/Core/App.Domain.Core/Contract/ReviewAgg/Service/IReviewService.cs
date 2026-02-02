using App.Domain.Core._common;
using App.Domain.Core.Contract.ReviewAgg.Repository;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Dtos.ReviewAgg;
using App.Domain.Core.Enums.ReviewAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.ReviewAgg.Service
{
    public interface IReviewService
    {
        public Task<Result<ReviewPagedDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
      
        public Task<Result<bool>> ChangeStatus(int id, ReviewStatus status, CancellationToken cancellationToken);
        public Task<Result<ReviewPagedDto>> GetByExpertId(int pageSize, int pageNumber, int expertId, CancellationToken cancellationToken);

        public Task<Result<bool>> Create(CreateReviewDto createReviewDto, CancellationToken cancellationToken);

        public Task<Result<bool>> HasCustomerCommentedOnOrder(int orderId, int customerId, CancellationToken cancellationToken);
    }
}

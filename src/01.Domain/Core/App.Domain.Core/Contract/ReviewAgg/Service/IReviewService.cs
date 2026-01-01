using App.Domain.Core._common;
using App.Domain.Core.Contract.ReviewAgg.Repository;
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
        public Task<Result<List<ReviewDto>>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
        public Task<int> GetCount(CancellationToken cancellationToken);

        public Task<Result<bool>> ChangeStatus(int id, ReviewStatus status, CancellationToken cancellationToken);
       
    }
}

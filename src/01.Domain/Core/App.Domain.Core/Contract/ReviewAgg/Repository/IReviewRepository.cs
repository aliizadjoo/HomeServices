using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Dtos.ReviewAgg;
using App.Domain.Core.Enums.ReviewAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.ReviewAgg.Repository
{
    public interface IReviewRepository
    {
        public Task<ReviewPagedDto> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
        public  Task<bool> ChangeStatus(int id, ReviewStatus status, CancellationToken cancellationToken);

        public Task<ReviewPagedDto> GetByExpertId(int pageSize, int pageNumber, int expertId, CancellationToken cancellationToken);
        public Task<int> GetExpertIdByReviewId(int reviewId, CancellationToken cancellationToken);
        public Task<double> AverageScore(int expertId, CancellationToken cancellationToken);


    }
}

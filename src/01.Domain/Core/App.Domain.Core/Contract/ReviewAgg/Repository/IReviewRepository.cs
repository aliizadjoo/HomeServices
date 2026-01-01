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
        public Task<List<ReviewDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);

        public Task<int> GetCount(CancellationToken cancellation);

        public  Task<bool> ChangeStatus(int id, ReviewStatus status, CancellationToken cancellationToken);




    }
}

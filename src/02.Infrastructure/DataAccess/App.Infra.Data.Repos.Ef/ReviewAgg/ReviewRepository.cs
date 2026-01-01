using App.Domain.Core.Contract.ReviewAgg.Repository;
using App.Domain.Core.Dtos.ReviewAgg;
using App.Domain.Core.Enums.ReviewAgg;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.ReviewAgg
{
    public class ReviewRepository(AppDbContext _context) : IReviewRepository
    {
        public async Task<ReviewPagedDto> GetAll(int pageNumber , int pageSize , CancellationToken cancellationToken) 
        {
           var query= _context.Reviews.AsQueryable();

           var totaCount = await query.CountAsync();

          var data= await query.AsNoTracking()
                .OrderBy(r => r.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new ReviewDto
                {
                    Id=o.Id,
                    Comment=o.Comment,
                    Score=o.Score,
                    OrderId =o.OrderId,
                    OrderDescription = o.Order.Description,
                    CustomerId =o.CustomerId,
                    CustomerFirstName = o.Customer.AppUser.FirstName,
                    CustomerLastName = o.Customer.AppUser.LastName,
                    ExpertId =o.ExpertId,
                    ExpertFirstName = o.Expert.AppUser.FirstName,
                    ExpertLastName = o.Expert.AppUser.LastName,
                    ImagePathExpert = o.Expert.AppUser.ImagePath,
                    ReviewStatus = o.ReviewStatus,
                    CreatedAt =o.CreatedAt,
                }).ToListAsync(cancellationToken);
            return new ReviewPagedDto
            {
                ReviewDtos = data,
                TotalCount = totaCount
            };

        }

    


        public async Task<bool> ChangeStatus(int id, ReviewStatus status, CancellationToken cancellationToken)
        {
            var rowsAffected = await _context.Reviews
                .Where(r => r.Id == id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(r => r.ReviewStatus, status),
                    cancellationToken);

            return rowsAffected > 0;
        }
    }
}

using App.Domain.Core.Contract.ReviewAgg.Repository;
using App.Domain.Core.Dtos.ReviewAgg;
using App.Domain.Core.Entities;
using App.Domain.Core.Enums.ReviewAgg;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

       public async Task<int> GetExpertIdByReviewId(int reviewId  , CancellationToken cancellationToken) 
       {

          return await _context.Reviews.Where(r => r.Id == reviewId)
                 .Select(r => r.ExpertId)
                 .FirstOrDefaultAsync(cancellationToken);
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

        public async Task<ReviewPagedDto> GetByExpertId(int pageSize, int pageNumber,int expertId ,  CancellationToken cancellationToken)
        {
            var query = _context.Reviews
                .AsNoTracking()
                .Where(r => r.ExpertId == expertId && r.ReviewStatus == ReviewStatus.Approved);

            var totalCount = await query.CountAsync(cancellationToken);

            var data= await query
                     .OrderByDescending(r => r.CreatedAt)
                     .Skip((pageNumber-1)*pageSize)
                     .Take(pageSize)
                     .Select(r=>new ReviewDto 
                     { 
                         Comment= r.Comment,
                         Score = r.Score,
                         HomeserviceName = r.Order.HomeService.Name,
                         CustomerFirstName = r.Customer.AppUser.FirstName,
                         CustomerLastName = r.Customer.AppUser.LastName,                      
                         CreatedAt = r.CreatedAt
                    
                     }).ToListAsync (cancellationToken);


                    return new ReviewPagedDto
                    {
                        TotalCount = totalCount,
                        ReviewDtos = data
                    };

        }

        public async Task<double> AverageScore(int expertId, CancellationToken cancellationToken)
        {
           return await _context.Reviews.Where(r=>r.ExpertId==expertId && r.ReviewStatus==ReviewStatus.Approved)
                  .AverageAsync(r=>(double?)r.Score , cancellationToken)??0;
        }

        public async Task<int> Create(CreateReviewDto createReviewDto, CancellationToken cancellationToken)
        {
            Review review = new Review() 
            {
                Comment = createReviewDto.Comment,
                Score = createReviewDto.Score,
                OrderId = createReviewDto.OrderId,
                CustomerId = createReviewDto.CustomerId,
                ExpertId = createReviewDto.ExpertId,
            
            };

           await _context.Reviews.AddAsync(review , cancellationToken);

           return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> HasCustomerCommentedOnOrder(int orderId, int customerId, CancellationToken cancellationToken)
        {
          return await  _context.Reviews
                 .AnyAsync(r => r.OrderId == orderId && r.CustomerId == customerId, cancellationToken);

        }
    }
}

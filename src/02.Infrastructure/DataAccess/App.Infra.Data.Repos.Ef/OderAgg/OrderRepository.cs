using App.Domain.Core.Contract.OrderAgg.Repository;
using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Dtos.ProposalAgg;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.OderAgg
{
    public class OrderRepository(AppDbContext _context) : IOrderRepository
    {

        public async Task<List<OrderDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Orders.AsQueryable();

            return await query.AsNoTracking()
                .OrderBy(o => o.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    Description = o.Description,
                    Status = o.Status,
                    ExecutionDate = o.ExecutionDate,
                    ExecutionTime = o.ExecutionTime,
                    CustomerId = o.CustomerId,
                    CustomerFirstName = o.Customer.AppUser.FirstName,
                    CustomerLastName = o.Customer.AppUser.LastName,
                    HomeServiceId = o.HomeServiceId,
                    HomeServiceName = o.HomeService.Name,
                    CityId = o.CityId,
                    CityName = o.City.Name,
                    ImagePaths = o.Images.Select(i => i.ImagePath).ToList(),
                    Proposals = o.Proposals.Select(p => new ProposalSummaryDto
                    {
                        Id = p.Id,
                        ExpertId = p.ExpertId,
                        ExpertFirstName = p.Expert.AppUser.FirstName,
                        ExpertLastName = p.Expert.AppUser.LastName,
                        Status = p.Status,
                        Price = p.Price,
                        Description = p.Description
                    }).ToList()
                })
                .ToListAsync(cancellationToken);
        }


        public async Task<int> GetCount(CancellationToken cancellationToken) 
        {
           return await _context.Orders.CountAsync(cancellationToken);
        }



    }
}

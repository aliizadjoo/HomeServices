using App.Domain.Core.Contract.OrderAgg.Repository;
using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Dtos.ProposalAgg;
using App.Domain.Core.Entities;
using App.Domain.Core.Enums.OrderAgg;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.OderAgg
{
    public class OrderRepository(AppDbContext _context) : IOrderRepository
    {
        public async Task<int> ChangeStatus(int orderId, OrderStatus newStatus , CancellationToken cancellationToken)
        {
            return await   _context.Orders.
                    Where(o => o.Id == orderId ).
                     ExecuteUpdateAsync(setter => setter
                      .SetProperty(o => o.Status, newStatus)
                      , cancellationToken
                     );
        }

        public async Task<int> Create(OrderCreateDto orderCreateDto, CancellationToken cancellationToken)
        {
            
            var order = new Order()
            {
                Description = orderCreateDto.Description,
                ExecutionDate = orderCreateDto.ExecutionDate,
                ExecutionTime = orderCreateDto.ExecutionTime,
                CustomerId = orderCreateDto.CustomerId,
                HomeServiceId = orderCreateDto.HomeServiceId,
                CityId = orderCreateDto.CityId,
                Images = orderCreateDto.ImagePaths.Select(path => new OrderImage
                {
                    ImagePath = path
                   
                }).ToList()
            };

           await _context.Orders.AddAsync(order, cancellationToken);
 
           
           return  await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<OrderPagedDtos> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Orders.AsQueryable();
            var totalCount = await query.CountAsync();

            var data = await query.AsNoTracking()
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
                    CityName = o.City.CityName,
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

            return new OrderPagedDtos
            {
                orderDtos = data,
                TotalCount = totalCount,
            };
        }

        public async Task<AvailableOrdersPagedDto> GetAvailableForExpertAsync(int expertId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {

            var expertInfo = await _context.Experts
                                  .Where(e => e.Id == expertId)
                                  .Select(e => new
                                  {
                                      e.CityId,
                                      ServiceIds = e.ExpertHomeServices
                                          .Select(s => s.HomeServiceId)
                                          .ToList()
                                  })
                                   .FirstOrDefaultAsync(cancellationToken);


            if (expertInfo == null) return new AvailableOrdersPagedDto();

            
            var query = _context.Orders
                                .AsNoTracking()
                                .Where(o => o.Status == OrderStatus.WaitingForProposals)
                                .Where(o => o.CityId == expertInfo.CityId)
                                .Where(o => expertInfo.ServiceIds.Contains(o.HomeServiceId));

            var totalCount = await query.CountAsync(cancellationToken);

            
            var data = await query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new AvailableOrderDto
                {
                    OrderId = o.Id,
                    CustomerId = o.CustomerId,
                    CustomerFullName = o.Customer.AppUser.FirstName + " " + o.Customer.AppUser.LastName,
                    Description = o.Description,
                    HomeServiceName = o.HomeService.Name,
                    BasePrice = o.HomeService.BasePrice,
                    ExecutionDate = o.ExecutionDate,
                    ExecutionTime = o.ExecutionTime,
                    Status = o.Status,
                    Images = o.Images.Select(img => img.ImagePath).ToList(),

                   
                    IsProposalSent = o.Proposals.Any(p => p.ExpertId == expertId)
                })
                .ToListAsync(cancellationToken);

            return new AvailableOrdersPagedDto
            {
                AvailableOrdersDto = data,
                TotalCount = totalCount
            };
        }

        public async Task<OrderSummaryDto?> GetOrderDetails(int orderId, CancellationToken cancellationToken)
        {
           return await _context.Orders.AsNoTracking()
                .Where(o=>o.Id==orderId)
                .Select(o=>new OrderSummaryDto 
                {
                   BasePrice = o.HomeService.BasePrice , 
                   HomeServiceName = o.HomeService.Name,

                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<decimal?> GetBasePriceByOrderId(int orderId , CancellationToken cancellationToken) 
        {

           return await _context.Orders.Where(o => o.Id == orderId)
                            .Select(o => o.HomeService.BasePrice)
                            .FirstOrDefaultAsync(cancellationToken);


        }

        public async Task<OrderPagedDtos> GetOrdersByAppUserId(int appUserId,int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Orders
                .Include(o => o.Customer)
                .Where(o => o.Customer.AppUserId == appUserId).AsQueryable();
             var totalCount = await query.CountAsync(cancellationToken);

            var data= await query.AsNoTracking()
                     .OrderBy(o=>o.CreatedAt)
                      .Skip((pageNumber - 1) * pageSize)
                     .Take(pageSize)
                     .Select(o => new OrderDto
                     { 
                        Id=o.Id,
                        Description = o.Description,                        
                        Status = o.Status,
                        ExecutionDate = o.ExecutionDate,
                        ExecutionTime = o.ExecutionTime,
                        CustomerId= o.CustomerId,    
                        CustomerFirstName=o.Customer.AppUser.FirstName,
                        CustomerLastName = o.Customer.AppUser.LastName,
                        HomeServiceId = o.HomeServiceId,
                        HomeServiceName= o.HomeService.Name,
                        CityId = o.CityId,
                        CityName = o.City.CityName,
                        ImagePaths = o.Images.Select(path => path.ImagePath).ToList(),
                        HasReview = o.Review != null
                     }).ToListAsync(cancellationToken);

            return new OrderPagedDtos
            {
                orderDtos = data,
                TotalCount = totalCount,

            };


        }

        public async Task<bool> IsExists(int orderId, CancellationToken cancellationToken)
        {
           return await _context.Orders.AnyAsync(o => o.Id == orderId, cancellationToken);
        }

        public async Task<OrderStatus> GetStatus(int orderId, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Where(o => o.Id == orderId)
                .Select(o => o.Status)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> SaveChanges(CancellationToken cancellationToken)
        {
          return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Order?> GetById(int orderId, CancellationToken cancellationToken)
        {
          return await  _context.Orders.FirstOrDefaultAsync(o=>o.Id== orderId, cancellationToken);  
        }

        public async Task<bool> IsPaid(int orderId, CancellationToken cancellationToken)
        {
            return await _context.Orders
            .AnyAsync(o => o.Id == orderId && o.PaymentStatus == PaymentStatus.Paid, cancellationToken);
        }

        public async Task<bool> IsOrderBelongToCustomer(int orderId, int customerId, CancellationToken cancellationToken)
        {
            return await _context.Orders
            .AsNoTracking()
            .AnyAsync(o =>
                o.Id == orderId &&
                o.CustomerId == customerId,
                cancellationToken);
        }


        public async Task<OrderSummaryDto?> GetOrderSummary(int orderId , CancellationToken cancellationToken) 
        {
         return await  _context.Orders.Where(o=>o.Id==orderId)
                             .Select(o=>new OrderSummaryDto() 
                             { 
                               BasePrice = o.HomeService.BasePrice,
                               HomeServiceId = o.HomeServiceId,
                               Status = o.Status,
                             }).FirstOrDefaultAsync(cancellationToken);

        }


    }
}

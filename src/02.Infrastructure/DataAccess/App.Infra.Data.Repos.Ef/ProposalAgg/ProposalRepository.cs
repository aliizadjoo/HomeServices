using App.Domain.Core.Contract.ProposalAgg.Repository;
using App.Domain.Core.Dtos.ProposalAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.ProposalAgg
{
    public class ProposalRepository(AppDbContext _context) : IProposalRepository
    {
        public async Task<int> Create(ProposalCreateDto proposalCreateDto, CancellationToken cancellationToken)
        {
           

          
            Proposal proposal = new Proposal() 
            {
               Price = proposalCreateDto.Price,
               Description = proposalCreateDto.Description,
               OrderId = proposalCreateDto.OrderId,
               ExpertId  = proposalCreateDto.ExpertId
            };

           await _context.Proposals.AddAsync(proposal , cancellationToken);
           return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ExpertProposalDto>> GetByExpertId( int expertId, CancellationToken cancellationToken)
        {
         return await   _context.Proposals.AsNoTracking().Where(p => p.ExpertId == expertId)
                .Select(p => new ExpertProposalDto
                {
                    ProposalId = p.Id,
                    OrderId =   p.OrderId,
                    HomeServiceName = p.Order.HomeService.Name,
                    CustomerFullName = p.Order.Customer.AppUser.FirstName +""+ p.Order.Customer.AppUser.LastName,
                    Price = p.Price,
                    ExecutionDate = p.Order.ExecutionDate,
                    ExecutionTime = p.Order.ExecutionTime,
                    Description = p.Description, 

                    Status = p.Status,

                }).ToListAsync(cancellationToken);
        }
    }

}

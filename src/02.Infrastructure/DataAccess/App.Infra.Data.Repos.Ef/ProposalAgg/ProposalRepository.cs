using App.Domain.Core._common;
using App.Domain.Core.Contract.ProposalAgg.Repository;
using App.Domain.Core.Dtos.ProposalAgg;
using App.Domain.Core.Entities;
using App.Domain.Core.Enums.OrderAgg;
using App.Domain.Core.Enums.ProposalAgg;
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
                ExpertId = proposalCreateDto.ExpertId
            };

            await _context.Proposals.AddAsync(proposal, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ExpertProposalDto>> GetByExpertId(int expertId, CancellationToken cancellationToken)
        {
            return await _context.Proposals.AsNoTracking().Where(p => p.ExpertId == expertId)
                   .Select(p => new ExpertProposalDto
                   {
                       ProposalId = p.Id,
                       OrderId = p.OrderId,
                       HomeServiceName = p.Order.HomeService.Name,
                       CustomerFullName = p.Order.Customer.AppUser.FirstName + "" + p.Order.Customer.AppUser.LastName,
                       Price = p.Price,
                       ExecutionDate = p.Order.ExecutionDate,
                       ExecutionTime = p.Order.ExecutionTime,
                       Description = p.Description,

                       Status = p.Status,

                   }).ToListAsync(cancellationToken);
        }

        public async Task<List<ProposalSummaryDto>> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await _context.Proposals.AsNoTracking()
                             .Where(p => p.OrderId == orderId)
                             .Select(p => new ProposalSummaryDto
                             {
                                 Id = p.Id,
                                 HomeServiceName = p.Order.HomeService.Name,
                                 AverageScore=p.Expert.AverageScore,
                                 ExecutionDate = p.Order.ExecutionDate,
                                 ExpertId = p.ExpertId,
                                 ExpertFirstName = p.Expert.AppUser.FirstName,
                                 ExpertLastName = p.Expert.AppUser.LastName,
                                 Status = p.Status,
                                 Price = p.Price,
                                 Description = p.Description,

                             })
                             .ToListAsync(cancellationToken);

        }

        public async Task<bool> IsRelatedToOrder(int proposalId, int orderId, CancellationToken cancellationToken)
        {
          return await  _context.Proposals.AnyAsync(p => p.OrderId == orderId && p.Id == proposalId, cancellationToken); 
        }

        public async Task<int> ChangeStatus(int proposalId, ProposalStatus newStatus, CancellationToken cancellationToken)
        {
            return await _context.Proposals
                .Where(p => p.Id == proposalId) 
                .ExecuteUpdateAsync(setter => setter.SetProperty(o => o.Status, newStatus), cancellationToken);
        }

        public async Task<bool> AnyAccepted(int orderId, CancellationToken cancellationToken)
        {
           return await _context.Proposals.AnyAsync(p=>p.OrderId==orderId && p.Status==ProposalStatus.Accepted , cancellationToken);

            
        }

        public async Task<bool> IsAlreadySubmitted(int expertId, int orderId, CancellationToken cancellationToken)
        {
           
            return await _context.Proposals
                .AnyAsync(p => p.ExpertId == expertId && p.OrderId == orderId, cancellationToken);
        }
        public async Task<int> RejectOtherProposals(int proposalId, int orderId, CancellationToken cancellationToken)
        {
            return await _context.Proposals
                .Where(p => p.OrderId == orderId && p.Id != proposalId && p.Status != ProposalStatus.Rejected)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(p => p.Status, ProposalStatus.Rejected),
                    cancellationToken);
        }

        public async Task<int> GetExpertIdByOrderId(int orderId, CancellationToken cancellationToken)
        {
          return  await  _context.Proposals
                 .Where(p => p.OrderId == orderId && p.Status == ProposalStatus.Accepted)
                 .Select(p => p.ExpertId)
                 .FirstOrDefaultAsync(cancellationToken);

        }


        public async Task<decimal> GetPriceByOrderId(int orderId, CancellationToken cancellationToken)
        {
          return await   _context.Proposals
                .Where(p=>p.OrderId==orderId &&  p.Status == ProposalStatus.Accepted) 
                .Select(p => p.Price)
                .FirstOrDefaultAsync(cancellationToken);
        }

    }
}

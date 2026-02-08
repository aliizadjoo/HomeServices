using App.Domain.Core._common;
using App.Domain.Core.Contract.ProposalAgg.AppService;
using App.Domain.Core.Contract.ProposalAgg.Service;
using App.Domain.Core.Dtos.ProposalAgg;
using App.Domain.Core.Enums.ProposalAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.ProposalAgg
{
    public class ProposalAppService(IProposalService _proposalService) : IProposalAppService
    {
        public async Task<Result<bool>> ChangeStatus(int proposalId, int orderId, ProposalStatus newStatus, CancellationToken cancellationToken)
        {
          return await _proposalService.ChangeStatus(proposalId, orderId, newStatus, cancellationToken);
        }

        public async Task<Result<bool>> Create(ProposalCreateDto proposalCreateDto, CancellationToken cancellationToken)
        {
          return await _proposalService.Create(proposalCreateDto, cancellationToken);
        }

        public async Task<Result<int>> GetExpertIdByOrderId(int orderId, CancellationToken cancellationToken)
        {
          return await  _proposalService.GetExpertIdByOrderId(orderId, cancellationToken);
        }

        public async Task<Result<List<ExpertProposalDto>>> GetExpertProposals(int expertId, CancellationToken cancellationToken)
        {
          return await  _proposalService.GetExpertProposals(expertId, cancellationToken);
        }

        public async Task<Result<List<ProposalSummaryDto>>> GetOrderProposals(int orderId, CancellationToken cancellationToken)
        {
          return  await _proposalService.GetOrderProposals(orderId, cancellationToken);
        }

        public async Task<bool> IsAlreadySubmitted(int expertId, int orderId, CancellationToken cancellationToken)
        {
           return await _proposalService.IsAlreadySubmitted(expertId, orderId, cancellationToken);
        }
    }
}

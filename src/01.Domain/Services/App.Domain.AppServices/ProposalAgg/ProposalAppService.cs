using App.Domain.Core._common;
using App.Domain.Core.Contract.ProposalAgg.AppService;
using App.Domain.Core.Contract.ProposalAgg.Service;
using App.Domain.Core.Dtos.ProposalAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.ProposalAgg
{
    public class ProposalAppService(IProposalService _proposalService) : IProposalAppService
    {
        public async Task<Result<bool>> Create(ProposalCreateDto proposalCreateDto, CancellationToken cancellationToken)
        {
          return await _proposalService.Create(proposalCreateDto, cancellationToken);
        }

        public async Task<Result<List<ExpertProposalDto>>> GetExpertProposals(int expertId, CancellationToken cancellationToken)
        {
          return await  _proposalService.GetExpertProposals(expertId, cancellationToken);
        }
    }
}

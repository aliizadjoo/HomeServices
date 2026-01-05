using App.Domain.Core._common;
using App.Domain.Core.Contract.ProposalAgg.Repository;
using App.Domain.Core.Dtos.ProposalAgg;
using App.Domain.Core.Enums.ProposalAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.ProposalAgg.AppService
{
    public interface IProposalAppService
    {
        public Task<Result<bool>> Create(ProposalCreateDto proposalCreateDto, CancellationToken cancellationToken);

        public Task<Result<List<ExpertProposalDto>>> GetExpertProposals(int expertId, CancellationToken cancellationToken);
        public Task<Result<List<ProposalSummaryDto>>> GetOrderProposals(int orderId, CancellationToken cancellationToken);

        public Task<Result<bool>> ChangeStatus(int proposalId, int orderId, ProposalStatus newStatus, CancellationToken cancellationToken);


    }
}

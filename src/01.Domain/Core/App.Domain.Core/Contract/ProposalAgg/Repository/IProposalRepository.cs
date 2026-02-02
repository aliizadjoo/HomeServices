using App.Domain.Core.Dtos.ProposalAgg;
using App.Domain.Core.Entities;
using App.Domain.Core.Enums.OrderAgg;
using App.Domain.Core.Enums.ProposalAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.ProposalAgg.Repository
{
    public interface IProposalRepository
    {

        public Task<int> Create(ProposalCreateDto proposalCreateDto, CancellationToken cancellationToken);

        public Task<List<ExpertProposalDto>> GetByExpertId(int expertId, CancellationToken cancellationToken);

        public Task<List<ProposalSummaryDto>> GetByOrderId(int orderId, CancellationToken cancellationToken);
        public Task<bool> IsRelatedToOrder(int proposalId, int orderId, CancellationToken cancellationToken);

        public Task<int> ChangeStatus(int proposalId, ProposalStatus newStatus, CancellationToken cancellationToken);
        public Task<int> GetExpertIdByOrderId(int orderId, CancellationToken cancellationToken);

        public Task<bool> AnyAccepted(int orderId, CancellationToken cancellationToken);

        public Task<bool> IsAlreadySubmitted(int expertId, int orderId, CancellationToken cancellationToken);

        public Task<int> RejectOtherProposals(int proposalId, int orderId, CancellationToken cancellationToken);
     
    }
}

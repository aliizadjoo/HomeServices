using App.Domain.Core.Dtos.ProposalAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.ProposalAgg.Repository
{
    public interface IProposalRepository
    {

        public Task<int> Create(ProposalCreateDto proposalCreateDto, CancellationToken cancellationToken);
    }
}

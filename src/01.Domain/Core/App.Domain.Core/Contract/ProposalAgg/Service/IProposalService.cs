using App.Domain.Core._common;
using App.Domain.Core.Contract.ProposalAgg.Repository;
using App.Domain.Core.Dtos.ProposalAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.ProposalAgg.Service
{
    public interface IProposalService
    {
        public Task<Result<bool>> Create(ProposalCreateDto proposalCreateDto, CancellationToken cancellationToken);
      
    }
}

using App.Domain.Core._common;
using App.Domain.Core.Dtos.ProposalAgg;
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
    }
}

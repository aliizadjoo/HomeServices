using App.Domain.Core._common;
using App.Domain.Core.Dtos.HomeServiceAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.HomeServiceAgg.Service
{
    public interface IHomeserviceService
    {
        public Task<Result<List<HomeserviceSummaryDto>>> GetAll( CancellationToken cancellationToken);
    }
}

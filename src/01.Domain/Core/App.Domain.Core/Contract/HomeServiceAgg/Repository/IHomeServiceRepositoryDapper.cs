using App.Domain.Core.Dtos.HomeServiceAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.HomeServiceAgg.Repository
{
    public interface IHomeServiceRepositoryDapper
    {

        public Task<List<HomeserviceDto>> GetAll(CancellationToken cancellationToken);
    }
}

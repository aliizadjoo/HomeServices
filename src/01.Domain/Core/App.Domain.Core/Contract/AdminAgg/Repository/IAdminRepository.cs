using App.Domain.Core.Dtos.AdminAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.AdminAgg.Repository
{
    public interface IAdminRepository
    {

        public Task<AdminPagedResultDto> GetAll( int pageNumber, int pageSize, CancellationToken cancellationToken);

        public Task<int> Create(CreateAdminDto adminDto, CancellationToken cancellationToken);
    }
}

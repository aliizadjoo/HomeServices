using App.Domain.Core._common;
using App.Domain.Core.Dtos.AdminAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.AdminAgg.Service
{
    public interface IAdminService
    {
        public Task<Result<AdminPagedResultDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);

        public  Task<Result<bool>> Create(int userId, CancellationToken cancellationToken);
    }
}

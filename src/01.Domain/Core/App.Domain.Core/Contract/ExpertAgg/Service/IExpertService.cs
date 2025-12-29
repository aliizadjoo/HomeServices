using App.Domain.Core._common;
using App.Domain.Core.Dtos.ExpertAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.ExpertAgg.Service
{
    public interface IExpertService
    {

        public Task<Result<ProfileExpertDto>> GetProfile(int expertId, CancellationToken cancellationToken);
        public  Task<Result<bool>> Create(int userId, int cityId, CancellationToken cancellationToken);
        
    }
}

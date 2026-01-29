using App.Domain.Core._common;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.ExpertAgg.Repository
{
    public interface IExpertRepository
    {
        public Task<ProfileExpertDto?> GetProfile(int expertId, CancellationToken cancellationToken);
        public  Task<int> Create(CreateExpertDto expertDto, CancellationToken cancellationToken);
        public Task<bool> ChangeProfile(int appUserId, ProfileExpertDto profileExpertDto, bool isAdmin, CancellationToken cancellationToken);
        public Task<ExpertPagedResultDto> GetAll( int pageNumber, int pageSize, CancellationToken cancellationToken);

        public Task<bool> Delete(int appUserId,  CancellationToken cancellationToken);

        public Task<int> GetIdByAppUserId(int appUserId, CancellationToken cancellationToken);

       public Task<bool> UpdateExpertScore(int expertId , double newAverageScore , CancellationToken cancellationToken);
 

    }
}

using App.Domain.Core._common;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.ExpertAgg.Repositorty
{
    public interface IExpertRepositoy
    {
        public Task<ProfileExpertDto?> GetProfile(int expertId, CancellationToken cancellationToken);
        public  Task<int> Create(CreateExpertDto expertDto, CancellationToken cancellationToken);
        public Task<bool> ChangeProfile(int appuserId, ProfileExpertDto profileExpertDto, CancellationToken cancellationToken);



    }
}

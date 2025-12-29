using App.Domain.Core._common;
using App.Domain.Core.Contract.ExpertAgg.Repositorty;
using App.Domain.Core.Contract.ExpertAgg.Service;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.ExpertAgg
{
    public class ExpertService
        (IExpertRepositoy _expertRepositoy 
        , ILogger<ExpertService> _logger) : IExpertService
    {
        public async Task<Result<bool>> Create(int userId, int cityId, CancellationToken cancellationToken)
        {
            CreateExpertDto createExperterDto = new CreateExpertDto()
            {
                AppUserId = userId,
                CityId = cityId
            };

            var result = await _expertRepositoy.Create(createExperterDto, cancellationToken);

            if (result <= 0)
                return Result<bool>.Failure("خطا در ایجاد پروفایل مشتری");

            return Result<bool>.Success(true);
        }

        public async Task<Result<ProfileExpertDto>> GetProfile(int appuserId, CancellationToken cancellationToken)
        {
           var profile=await _expertRepositoy.GetProfile(appuserId, cancellationToken);
            if (profile == null)
            {
                _logger.LogWarning("کارشناس با کد {ExpertId} در سیستم یافت نشد.", appuserId);
                return Result<ProfileExpertDto>.Failure("متخصص مورد نظر یافت نشد.");
            }
            _logger.LogInformation("اطلاعات پروفایل کارشناس {ExpertId} با موفقیت بارگذاری شد.", appuserId);
            return Result<ProfileExpertDto>.Success(profile);
        }

        
    }
}

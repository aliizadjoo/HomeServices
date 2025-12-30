using App.Domain.Core._common;
using App.Domain.Core.Contract.ExpertAgg.Repositorty;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.ExpertAgg
{
    public class ExpertRepositoy(AppDbContext _context) : IExpertRepositoy
    {


        public async Task<ProfileExpertDto?> GetProfile(int appuserId, CancellationToken cancellationToken)
        {
            return await _context.Experts
                .AsNoTracking() 
                .Where(e => e.AppUserId == appuserId)
                .Select(e => new ProfileExpertDto
                {
                    FirstName = e.AppUser.FirstName,
                    LastName = e.AppUser.LastName,
                    Bio = e.Bio,
                    ImagePath = e.AppUser.ImagePath,
                    WalletBalance = e.WalletBalance,
                    CityId = e.CityId,
                    CityName = e.City.Name,
                    HomeServicesId = e.ExpertHomeServices
                        .Select(ehs => ehs.HomeServiceId)
                        .ToList(),
                    HomeServices = e.ExpertHomeServices
                        .Select(ehs => ehs.HomeService.Name)
                        .ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> Create(CreateExpertDto expertDto, CancellationToken cancellationToken)
        {
            var expert = new Expert()
            {
                CityId = expertDto.CityId,
                AppUserId = expertDto.AppUserId,
            };

            await _context.Experts.AddAsync(expert, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task<bool> ChangeProfile(int appuserId, ProfileExpertDto profileExpertDto, CancellationToken ct)
        {
            var expert = await _context.Experts
                .Include(e => e.AppUser)
                .Include(e => e.ExpertHomeServices).IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => e.AppUserId == appuserId, ct);

            if (expert == null) return false;

           
            expert.AppUser.FirstName = profileExpertDto.FirstName;
            expert.AppUser.LastName = profileExpertDto.LastName;
            expert.AppUser.ImagePath = profileExpertDto.ImagePath;
            expert.Bio = profileExpertDto.Bio;
            expert.CityId = profileExpertDto.CityId;

         
            var toDelete = expert.ExpertHomeServices
                .Where(ehs => !ehs.IsDeleted && !profileExpertDto.HomeServicesId.Contains(ehs.HomeServiceId));

            foreach (var item in toDelete)
            {
                item.IsDeleted = true;
            }

            foreach (var serviceId in profileExpertDto.HomeServicesId)
            {
                var existing = expert.ExpertHomeServices
                    .FirstOrDefault(ehs => ehs.HomeServiceId == serviceId);

                if (existing != null)
                {
                   
                    if (existing.IsDeleted)
                    {
                        existing.IsDeleted = false;
                    }
                  
                }
                else
                {
               
                    expert.ExpertHomeServices.Add(new ExpertHomeService
                    {
                        ExpertId = expert.Id,
                        HomeServiceId = serviceId,
                        IsDeleted = false
                    });
                }
            }

           
            return await _context.SaveChangesAsync(ct) > 0;
        }
    }
}

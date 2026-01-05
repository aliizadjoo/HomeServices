using App.Domain.Core._common;
using App.Domain.Core.Contract.ExpertAgg.Repository;
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
    public class ExpertRepositoy(AppDbContext _context) : IExpertRepository
    {


        public async Task<ProfileExpertDto?> GetProfile(int appuserId, CancellationToken cancellationToken)
        {
            return await _context.Experts
                .AsNoTracking() 
                .Where(e => e.AppUserId == appuserId)
                .Select(e => new ProfileExpertDto
                {
                    AppUserId = appuserId,
                    Email= e.AppUser.Email,
                    FirstName = e.AppUser.FirstName,
                    LastName = e.AppUser.LastName,
                    AverageScore = e.AverageScore,
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

        public async Task<bool> ChangeProfile(int appuserId, ProfileExpertDto profileExpertDto, bool isAdmin, CancellationToken ct)
        {
           
            var expert = await _context.Experts
                .Include(e => e.AppUser)
                .Include(e => e.ExpertHomeServices)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => e.AppUserId == appuserId, ct);

            if (expert == null) return false;

          
            expert.AppUser.FirstName = profileExpertDto.FirstName;
            expert.AppUser.LastName = profileExpertDto.LastName;
            expert.AppUser.ImagePath = profileExpertDto.ImagePath;

        
            expert.Bio = profileExpertDto.Bio;
            expert.CityId = profileExpertDto.CityId;

         
            if (isAdmin && profileExpertDto.WalletBalance.HasValue)
            {
                expert.WalletBalance = profileExpertDto.WalletBalance.Value;
            }

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

        public async Task<ExpertPagedResultDto> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            
            var query = _context.Experts
                .AsNoTracking()
                .AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var experts = await query.OrderBy(e=>e.Id)
                .Select(e => new ExpertListDto
                {
                    ExpertId = e.Id,
                    AppUserId = e.AppUserId,
                    FirstName = e.AppUser.FirstName,
                    LastName = e.AppUser.LastName,
                    Email = e.AppUser.Email,
                    CityName = e.City.Name,
                    ServiceNames = e.ExpertHomeServices
                          .Select(ehs => ehs.HomeService.Name)
                          .ToList(),
                    WalletBalance = e.WalletBalance,
                    AverageScore = e.AverageScore,
                    CreatedAt = e.CreatedAt
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);


            return new ExpertPagedResultDto
            {
                Experts = experts,
                TotalCount = totalCount
            };
        }

        public async Task<bool> Delete(int appUserId,  CancellationToken cancellationToken)
        {
          
          
            var rowsAffectedExpert = await _context.Experts
                .Where(e => e.AppUserId == appUserId )
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(e => e.IsDeleted, true),
                    cancellationToken);

            
            return rowsAffectedExpert > 0;
        }

        public async Task<int> GetIdByAppUserId(int appUserId, CancellationToken cancellationToken)
        {
            return await _context.Experts
                            .Where(e => e.AppUserId == appUserId)
                            .Select(e => e.Id) 
                            .FirstOrDefaultAsync(cancellationToken);
        }

     
    }
}

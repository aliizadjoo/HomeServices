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
                .Where(e => e.AppUserId == appuserId)
                .Select(e => new ProfileExpertDto
                {
                    Bio = e.Bio,
                    WalletBalance = e.WalletBalance,
                    FirstName = e.AppUser.FirstName,
                    LastName = e.AppUser.LastName,
                    ImagePath = e.AppUser.ImagePath,
                    CityName = e.City.Name,
                    HomeServices = e.HomeServices.Select(hs => hs.Name).ToList()

                }).FirstOrDefaultAsync(cancellationToken);
                
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
    }
}

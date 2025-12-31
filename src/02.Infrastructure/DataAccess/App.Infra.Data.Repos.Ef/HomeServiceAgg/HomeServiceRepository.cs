using App.Domain.Core.Contract.HomeServiceAgg.Repository;
using App.Domain.Core.Dtos.HomeServiceAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.HomeServiceAgg
{
    public class HomeServiceRepository(AppDbContext _context) : IHomeserviceRepository
    {
        public async Task<int> Create(CreateHomeServiceDto homeServiceDto, CancellationToken cancellationToken)
        {
            var homeService = new HomeService
            {
                Name = homeServiceDto.Name,
                Description = homeServiceDto.Description,
                ImagePath = homeServiceDto.ImagePath,
                BasePrice = homeServiceDto.BasePrice,
                CategoryId = homeServiceDto.CategoryId
            };

            await _context.HomeServices.AddAsync(homeService, cancellationToken);
            return  await _context.SaveChangesAsync(cancellationToken);
 


        }

        public async Task<List<HomeserviceSummaryDto>> GetAll(CancellationToken cancellationToken)
        {
           return await _context.HomeServices
                 .AsNoTracking()
                 .Select(hs => new HomeserviceSummaryDto
                 {
                     HomeservicesId = hs.Id,
                     Name = hs.Name,
                 }).ToListAsync(cancellationToken);
        }

        public async Task<List<HomeserviceDto>> GetAll(int pageSize, int pageNumber, SearchHomeServiceDto search, CancellationToken cancellationToken)
        {
            var query = _context.HomeServices
                        .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(hs => hs.Name.Contains(search.Name) );
            }

            if (search.CategoryId!=null)
            {
                query = query.Where(hs => hs.CategoryId == search.CategoryId);
            }
            return await query.OrderBy(hs => hs.Id)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .Select(hs => new HomeserviceDto()
                 {
                     Id = hs.Id,
                     Name = hs.Name,
                     Description = hs.Description,
                     ImagePath = hs.ImagePath,
                     BasePrice = hs.BasePrice,
                     CategoryId = hs.CategoryId,
                     CategoryName = hs.Category.Title

                 }).ToListAsync(cancellationToken);

        }

        public async Task<HomeserviceDto?> GetById(int homeServiceId, CancellationToken cancellationToken)
        {
            return await _context.HomeServices
               .AsNoTracking()
               .Where(hs => hs.Id == homeServiceId )
               .Select(hs => new HomeserviceDto
               {
                   Id = hs.Id,
                   Name = hs.Name,
                   Description = hs.Description,
                   ImagePath = hs.ImagePath,
                   BasePrice = hs.BasePrice,
                   CategoryId = hs.CategoryId,
                   CategoryName = hs.Category.Title
               })
               .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<bool> Update(HomeserviceDto dto, CancellationToken cancellationToken)
        {
            var rowsAffected = await _context.HomeServices
                .Where(hs => hs.Id == dto.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.Name, dto.Name)
                    .SetProperty(p => p.Description, dto.Description)
                    .SetProperty(p => p.BasePrice, dto.BasePrice)
                    .SetProperty(p => p.ImagePath, dto.ImagePath)
                    .SetProperty(p => p.CategoryId, dto.CategoryId),
                    cancellationToken);

            return rowsAffected > 0;
        }

        public async Task<int> GetCount(CancellationToken cancellationToken)
        {
           
            return await _context.HomeServices.CountAsync(cancellationToken);
        }


        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var rowsAffected = await _context.HomeServices
                .Where(h => h.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(h => h.IsDeleted, true),
                    cancellationToken);

            return rowsAffected > 0;
        }


    }
}

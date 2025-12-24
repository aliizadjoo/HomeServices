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
    public class HomeServiceRepository(AppDbContext _context) : IHomeServiceRepository
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
            await _context.SaveChangesAsync(cancellationToken);


            return homeService.Id;


        }

        public async Task<List<HomeServiceDto>> GetAll(int pageSize, int pageNumber, SearchHomeServiceDto search, CancellationToken cancellationToken)
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
                 .Select(hs => new HomeServiceDto()
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

        public async Task<HomeServiceDto?> GetById(int homeServiceId, CancellationToken cancellationToken)
        {
            return await _context.HomeServices
               .AsNoTracking()
               .Where(hs => hs.Id == homeServiceId )
               .Select(hs => new HomeServiceDto
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

        public async Task<int> Update(HomeServiceDto homeServiceDto, CancellationToken cancellationToken)
        {
          return await  _context.HomeServices
                    .Where(hs=>hs.Id==homeServiceDto.Id)
                    .ExecuteUpdateAsync(setter=>setter
                        .SetProperty(hs=>hs.Name,homeServiceDto.Name)
                        .SetProperty(hs=>hs.Description,homeServiceDto.Description)
                        .SetProperty(hs=>hs.ImagePath,homeServiceDto.ImagePath )
                        .SetProperty(hs=>hs.BasePrice,homeServiceDto.BasePrice)
                        .SetProperty(hs=>hs.CategoryId,homeServiceDto.CategoryId)
                        ,cancellationToken);
        }


    }
}

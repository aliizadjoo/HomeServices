using App.Domain.Core.Contract.HomeServiceAgg.Repository;
using App.Domain.Core.Dtos.HomeServiceAgg;
using App.Domain.Core.Entities;
using App.Infra.Cache;
using App.Infra.Cache.Contracts;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.HomeServiceAgg
{
    public class HomeServiceRepository(AppDbContext _context , ICacheService _cacheService) : IHomeserviceRepository
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

        public async Task<List<HomeserviceDto>> GetAll(CancellationToken cancellationToken)
        {

            var cachedHomeserviceDtos=_cacheService.Get<List<HomeserviceDto>>(CacheKeys.Homeservices);

            if (cachedHomeserviceDtos ==null)
            {

                var homeserviceDtos= await _context.HomeServices
                                                   .AsNoTracking()
                                                   .Select(hs => new HomeserviceDto
                                                   {   ImagePath = hs.ImagePath,
                                                       Id = hs.Id,
                                                       Name = hs.Name,
                                                       CategoryName = hs.Category.Title,
                                                       BasePrice = hs.BasePrice,
                                                       Description = hs.Description,
                                                       CategoryId = hs.CategoryId
                                                   }).ToListAsync(cancellationToken);


                _cacheService.SetSliding<List<HomeserviceDto>>(CacheKeys.Homeservices, homeserviceDtos, 30);

                return homeserviceDtos;
            }
              
            return cachedHomeserviceDtos;
       
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

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var rowsAffected = await _context.HomeServices
                .Where(h => h.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(h => h.IsDeleted, true),
                    cancellationToken);

            return rowsAffected > 0;
        }

        public async Task<HomeservicePagedDto> GetServicesByCategoryId(int categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
           
            var query = _context.HomeServices
                .Where(hs => hs.CategoryId == categoryId ); 

            var totalCount = await query.CountAsync(cancellationToken);

            var data = await query.AsNoTracking()
                .OrderBy(hs => hs.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(hs => new HomeserviceDto
                {
                    Id = hs.Id,
                    Name = hs.Name,
                    CategoryName = hs.Category.Title,
                    CategoryId = categoryId,
                    BasePrice = hs.BasePrice,
                    Description = hs.Description,
                    ImagePath = hs.ImagePath,
                }).ToListAsync(cancellationToken);

            return new HomeservicePagedDto
            {
                HomeserviceDtos = data,
                TotalCount = totalCount,
            };
        }


    }
}

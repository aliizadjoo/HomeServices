using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Dtos.AdminAgg;
using App.Domain.Core.Dtos.CategoryAgg;
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

namespace App.Infra.Data.Repos.Ef.CategoryAgg
{
    public class CategoryRepository(AppDbContext _context , ICacheService _cacheService ) : ICategoryRepository
    {
        public async Task<int> Create(string title, string imagePath, CancellationToken cancellationToken)
        {
            var category = new Category()
            {
                Title = title,
                ImagePath = imagePath
            };

            await _context.Categories.AddAsync(category, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
             


        }

        public async Task<CategoryDto?> GetById(int categoryId, CancellationToken cancellationToken)
        {
            return await _context.Categories
                       .AsNoTracking()
                       .Where(c => c.Id == categoryId)
                       .Select(c => new CategoryDto()
                       {
                           Id = c.Id,
                           Title = c.Title,
                           ImagePath = c.ImagePath,

                       }).FirstOrDefaultAsync(cancellationToken);
        }

        
        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
           var cachedCategoryDtos= _cacheService.Get<List<CategoryDto>>(CacheKeys.Categories);
            if (cachedCategoryDtos==null)
            {
                var categoryDtos=await _context.Categories
                                    .AsNoTracking()
                                    .Select(c => new CategoryDto
                                    {
                                        Id = c.Id,
                                        Title = c.Title,
                                        ImagePath = c.ImagePath,
                                    }).ToListAsync(cancellationToken);

                _cacheService.SetSliding<List<CategoryDto>>(CacheKeys.Categories , categoryDtos , 30);

                return categoryDtos;
            }
            return cachedCategoryDtos;
          
        }

        public async Task<List<CategoryWithHomeServices>> GetAllWithHomeServices(CancellationToken cancellationToken)
        {
            var cachedCategoryWithHomservicesDtos = _cacheService.Get<List<CategoryWithHomeServices>>(CacheKeys.CategoriesWithHomeservices);

            if (cachedCategoryWithHomservicesDtos==null)
            {
                var categoryWithHomservicesDtos =await  _context.Categories
                    .AsNoTracking()
                    .Select(c=> new CategoryWithHomeServices 
                    { 
                        Id = c.Id,
                        Title = c.Title,
                        ImagePath = c.ImagePath,
                        HomeserviceDtos = c.Services.Select(s=>new HomeserviceDto 
                        {
                            Id = s.Id,
                            Name = s.Name,
                            BasePrice = s.BasePrice,
                            Description = s.Description,
                            ImagePath = s.ImagePath,
                            CategoryName = s.Category.Title,
                            CategoryId = s.CategoryId
                         
                        }).ToList()
                    
                    }).ToListAsync(cancellationToken);

                _cacheService.SetSliding<List<CategoryWithHomeServices>>(CacheKeys.CategoriesWithHomeservices, categoryWithHomservicesDtos, 30);

                return categoryWithHomservicesDtos;
            }

            return cachedCategoryWithHomservicesDtos;
        }
        public async Task<bool> Update(CategoryDto categoryDto, CancellationToken cancellationToken)
        {

            var categoryRows= _context.Categories
                .Where(c => c.Id == categoryDto.Id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(c => c.Title, categoryDto.Title)
                    .SetProperty(c => c.ImagePath,  categoryDto.ImagePath ),
                    cancellationToken);

            return await categoryRows > 0;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var affectedRows = await _context.Categories
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(c => c.IsDeleted, true),
                    cancellationToken);

            return affectedRows > 0;
        }

       
    }
}

using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Dtos.AdminAgg;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.CategoryAgg
{
    public class CategoryRepository(AppDbContext _context) : ICategoryRepository
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

        public async Task<CategoryPagedDto> GetAll(int pageSize, int pageNumber, string? search, CancellationToken cancellationToken)
        {
            var query = _context.Categories
                        .AsNoTracking();
            var totalCount = await query.CountAsync(cancellationToken);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.Title.Contains(search));
            }
            

            var data = await query.OrderBy(c => c.Id)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .Select(c => new CategoryDto()
                 {
                     Id = c.Id,
                     Title = c.Title,
                     ImagePath = c.ImagePath,
                 }).ToListAsync(cancellationToken);


            return new CategoryPagedDto
            {
                CategoryDtos = data,
                TotalCount = totalCount
            };


        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Categories
                .AsNoTracking()
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    ImagePath = c.ImagePath,
                }).ToListAsync(cancellationToken);
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

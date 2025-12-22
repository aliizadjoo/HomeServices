using App.Domain.Core.Contract.CategoryAgg.Repository;
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
        public async Task<int> Create(string title, string? imagePath, CancellationToken cancellationToken)
        {
            var category = new Category()
            {
                Title = title,
                ImagePath = imagePath
            };

            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return category.Id;


        }

        public async Task<CategoryDto?> GerById(int categoryId, CancellationToken cancellationToken)
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

        public async Task<List<CategoryDto>> GetAll(int pageSize, int pageNumber, string? search, CancellationToken cancellationToken)
        {
            var query = _context.Categories
                        .AsNoTracking();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Title.Contains(search));

            }

            return await query.OrderBy(c => c.Id)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .Select(c => new CategoryDto()
                 {
                     Id = c.Id,
                     Title = c.Title,
                     ImagePath = c.ImagePath,
                 }).ToListAsync(cancellationToken);


        }

        public async Task<int> Update(CategoryDto categoryDto, CancellationToken cancellationToken)
        {

            return await _context.Categories
                .Where(c => c.Id == categoryDto.Id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(c => c.Title, categoryDto.Title)
                    .SetProperty(c => c.ImagePath, categoryDto.ImagePath),
                    cancellationToken);
        }

    }
}

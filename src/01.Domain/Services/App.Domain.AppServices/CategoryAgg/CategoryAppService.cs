using App.Domain.Core._common;
using App.Domain.Core.Contract.CategoryAgg.AppService;
using App.Domain.Core.Contract.CategoryAgg.Service;
using App.Domain.Core.Dtos.CategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.CategoryAgg
{
    public class CategoryAppService(ICategoryService _categoryService) : ICategoryAppService
    {
        public async Task<Result<List<CategoryDto>>> GetAll(CancellationToken cancellationToken)
        {
            
            return await _categoryService.GetAll(cancellationToken);
        }

        public async Task<Result<bool>> Update(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
          return await _categoryService.Update(categoryDto, cancellationToken);
        }

        public async Task<Result<CategoryDto>> GetById(int id, CancellationToken cancellationToken)
        {
          
           return await _categoryService.GetById(id, cancellationToken);

        }

        public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
        {
            return await _categoryService.Delete(id, cancellationToken);
        }

        public async Task<Result<int>> Create(string title, string imagePath, CancellationToken cancellationToken)
        {
            return await _categoryService.Create(title, imagePath, cancellationToken);
        }
    }
}

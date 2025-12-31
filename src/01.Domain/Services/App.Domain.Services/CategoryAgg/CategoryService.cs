using App.Domain.Core._common;
using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Dtos.CategoryAgg;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CategoryAgg.Service
{
    public class CategoryService(ICategoryRepository _categoryRepository, ILogger<CategoryService> _logger) : ICategoryService
    {
        public async Task<Result<List<CategoryDto>>> GetAll(CancellationToken cancellationToken)
        {
            _logger.LogInformation("دریافت لیست تمامی دسته‌بندی‌ها بدون پجینیشن");

            var categories = await _categoryRepository.GetAll(cancellationToken);

           
            return Result<List<CategoryDto>>.Success(categories);
        }


    }
}

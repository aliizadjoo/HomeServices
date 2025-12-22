using App.Domain.Core.Dtos.CategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CategoryAgg.Repository
{
    public interface ICategoryRepository
    {

        public Task<int> Create(string title , string? imagePath , CancellationToken cancellationToken);

        public Task<int> Update(CategoryDto categoryDto , CancellationToken cancellationToken);

        public Task<CategoryDto?> GerById(int categoryId , CancellationToken cancellationToken);

        public Task<List<CategoryDto>> GetAll(int pageSize , int pageNumber , string? search, CancellationToken cancellationToken);
    }
}

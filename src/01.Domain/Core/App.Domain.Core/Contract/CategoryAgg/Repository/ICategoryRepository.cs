using App.Domain.Core.Dtos.CategoryAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CategoryAgg.Repository
{
    public interface ICategoryRepository
    {

        public Task<int> Create(string title , string imagePath , CancellationToken cancellationToken);

        public Task<bool> Update(CategoryDto categoryDto , CancellationToken cancellationToken);

        public Task<CategoryDto?> GetById(int categoryId , CancellationToken cancellationToken);

        public  Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken);

        public Task<List<CategoryDto>> GetAll(int pageSize , int pageNumber , string? search, CancellationToken cancellationToken);
        public Task<bool> Delete(int id, CancellationToken cancellationToken);

        public Task<int> GetCount(CancellationToken cancellationToken);




    }
}

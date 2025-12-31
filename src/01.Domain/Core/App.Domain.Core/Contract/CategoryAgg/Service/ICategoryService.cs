using App.Domain.Core._common;
using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Dtos.CategoryAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CategoryAgg.Service
{
    public interface ICategoryService
    {

        public Task<Result<List<CategoryDto>>> GetAll(int pageSize, int pageNumber, string? search, CancellationToken cancellationToken);
        public Task<Result<List<CategoryDto>>> GetAll( CancellationToken cancellationToken);

        public Task<Result<bool>> Update(CategoryDto categoryDto, CancellationToken cancellationToken);

        public  Task<Result<CategoryDto>> GetById(int id, CancellationToken cancellationToken);

        public  Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);

        public Task<Result<int>> Create(string title, string imagePath, CancellationToken cancellationToken);
        public Task<int> GetCount(CancellationToken cancellationToken);
       



    }
}

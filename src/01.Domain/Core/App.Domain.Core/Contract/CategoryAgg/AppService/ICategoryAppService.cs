using App.Domain.Core._common;
using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Contract.CategoryAgg.Service;
using App.Domain.Core.Dtos.CategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CategoryAgg.AppService
{
    public interface ICategoryAppService
    {
        public Task<Result<CategoryPagedDto>> GetAll(int pageSize, int pageNumber, string? search, CancellationToken cancellationToken);
        public Task<Result<List<CategoryDto>>> GetAll( CancellationToken cancellationToken);
        public Task<Result<bool>> Update(CategoryDto categoryDto, CancellationToken cancellationToken);

        public Task<Result<CategoryDto>> GetById(int id, CancellationToken cancellationToken);


        public Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);

        public Task<Result<int>> Create(string title, string imagePath, CancellationToken cancellationToken);

    




    }
}

using App.Domain.Core.Dtos.CategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CategoryAgg.Repository
{
    public interface ICategoryRepositoryDapper
    {
        public  Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken);
    }
}

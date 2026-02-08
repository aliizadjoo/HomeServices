using App.Domain.Core.Configurations;
using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Infra.Cache;
using App.Infra.Cache.Contracts;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Dapper.CategoryAgg
{
    public class CategoryRepositoryDapper (SiteSetting siteSetting , ICacheService _cacheService) : ICategoryRepositoryDapper
    {
        private readonly string _connectionString = siteSetting.ConnectionStrings.Sql;


        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            
            var cachedCategoryDtos = _cacheService.Get<List<CategoryDto>>(CacheKeys.Categories);
            if (cachedCategoryDtos != null)
            {
                return cachedCategoryDtos;
            }

       
            using (var connection = new SqlConnection(_connectionString))
            {

                var command = new CommandDefinition(CategoryQueries.GetAll, cancellationToken);
              
                var result = await connection.QueryAsync<CategoryDto>(command);

                var categoryDtos = result.ToList();

               
                _cacheService.SetSliding<List<CategoryDto>>(CacheKeys.Categories, categoryDtos, 30);

                return categoryDtos;
            }
        }
    }
}
   
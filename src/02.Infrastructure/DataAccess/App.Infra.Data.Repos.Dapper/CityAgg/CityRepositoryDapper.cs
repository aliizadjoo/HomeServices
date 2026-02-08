using App.Domain.Core.Configurations;
using App.Domain.Core.Contract.CityAgg.Repository;
using App.Domain.Core.Dtos.CityAgg;
using App.Infra.Cache;
using App.Infra.Cache.Contracts;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Dapper.CityAgg
{
    public class CityRepositoryDapper(SiteSetting siteSetting , ICacheService _cacheService) : ICityRepositoryDapper
    {
        private readonly string _connectionString = siteSetting.ConnectionStrings.Sql;
        public async Task<List<CityDto>> GetAll(CancellationToken cancellationToken)
        {
            var cachedCityDtos = _cacheService.Get<List<CityDto>>(CacheKeys.Cities);
            if (cachedCityDtos !=null)
            {
                return cachedCityDtos;
            }

            using (var connection = new SqlConnection (_connectionString)) 
            {
                var command = new CommandDefinition(CityQueries.GetAll, cancellationToken);
                var result = await connection.QueryAsync<CityDto>(command);

                var cityDtos = result.ToList();

                _cacheService.SetSliding<List<CityDto>>(CacheKeys.Cities, cityDtos, 30);

                return cityDtos;
            }
        }
    }
}

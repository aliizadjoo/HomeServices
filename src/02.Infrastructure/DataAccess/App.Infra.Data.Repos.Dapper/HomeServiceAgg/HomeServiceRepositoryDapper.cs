using App.Domain.Core.Configurations;
using App.Domain.Core.Contract.HomeServiceAgg.Repository;
using App.Domain.Core.Dtos.HomeServiceAgg;
using App.Infra.Cache;
using App.Infra.Cache.Contracts;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Dapper.HomeServiceAgg
{
    public class HomeServiceRepositoryDapper(SiteSetting siteSetting , ICacheService _cacheService) : IHomeServiceRepositoryDapper
    {
        private readonly string _connectionString = siteSetting.ConnectionStrings.Sql;
        public async Task<List<HomeserviceDto>> GetAll(CancellationToken cancellationToken)
        {
            var cachedHomeserviceDtos= _cacheService.Get<List<HomeserviceDto>>(CacheKeys.Homeservices);
            if (cachedHomeserviceDtos !=null)
            {
                return cachedHomeserviceDtos;
            }

            using (var connection = new SqlConnection(_connectionString)) 
            {

                var command = new CommandDefinition(HomeServiceQueries.GetAll, cancellationToken);

                var result= await connection.QueryAsync<HomeserviceDto>(command);
                var HomeserviceDtos = result.ToList();

                _cacheService.SetSliding<List<HomeserviceDto>>(CacheKeys.Homeservices, HomeserviceDtos, 30);
                return HomeserviceDtos;

            }
        }
    }
}

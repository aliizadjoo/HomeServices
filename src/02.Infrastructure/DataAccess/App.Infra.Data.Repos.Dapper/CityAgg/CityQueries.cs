using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Dapper.CityAgg
{
    public static class CityQueries
    {
        public const string GetAll = "Select Id ,CityName From Cities where IsDeleted = 0";
    }
}

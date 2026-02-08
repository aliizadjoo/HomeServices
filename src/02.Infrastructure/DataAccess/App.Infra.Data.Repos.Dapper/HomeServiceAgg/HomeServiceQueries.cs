using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Dapper.HomeServiceAgg
{
    public static class HomeServiceQueries
    {
        public const string GetAll = @"Select 
                                      hs.Id , hs.Name , c.Title As CategoryName , hs.BasePrice , hs.Description , hs.ImagePath ,  hs.CategoryId
                                      From HomeServices hs 
                                      LEFT JOIN Categories c On hs.CategoryId = c.Id  
                                      where hs.isDeleted=0";




    }
}

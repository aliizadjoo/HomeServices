using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CategoryAgg.Repository
{
    public interface ICategoryRepository
    {

        public Task<int> Create(string title , string imagePath);


    }
}

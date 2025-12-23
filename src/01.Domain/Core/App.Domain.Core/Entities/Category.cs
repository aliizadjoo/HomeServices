using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }
        public string ImagePath { get; set; } 
        public List<HomeService> Services { get; set; } = [];
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class HomeService : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public List<Order> Orders { get; set; } = [];
        public List<ExpertHomeService> ExpertHomeServices { get; set; } = [];
    }
}

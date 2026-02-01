using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.HomeServiceAgg
{
    public class HomeserviceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string CategoryName { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }
        public int CategoryId { get; set; }
    }

  
}

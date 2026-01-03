using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.OrderAgg
{
    public class OrderCreateDto
    {

        public int HomeServiceId { get; set; }

        
        public int AppUserId { get; set; }

        public string Description { get; set; }

       
        public DateTime ExecutionDate { get; set; }

      
        public TimeSpan ExecutionTime { get; set; }

       
        public int CityId { get; set; }

       
        public List<string> ImagePaths { get; set; } = [];
    }
}

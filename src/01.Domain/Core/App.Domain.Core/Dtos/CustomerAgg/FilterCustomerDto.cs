using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.CustomerAgg
{
    public class FilterCustomerDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

       
        public int? CityId { get; set; }
        public string? CityName { get; set; }

       
        public string? CreatedFrom { get; set; } 
        public string? CreatedTo { get; set; }

     
    }
}

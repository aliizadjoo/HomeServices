using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.CustomerAgg
{
    public class CustomerListDto
    {

        public int CustomerId { get; set; } 
        public int AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string CityName { get; set; }
        public decimal WalletBalance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

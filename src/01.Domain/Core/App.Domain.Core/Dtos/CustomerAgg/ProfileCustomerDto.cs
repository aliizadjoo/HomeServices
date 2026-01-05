using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.CustomerAgg
{
    public class ProfileCustomerDto
    {
        public int AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImagePath { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string? Email { get; set; }
        public decimal? WalletBalance { get; set; }
    }
}

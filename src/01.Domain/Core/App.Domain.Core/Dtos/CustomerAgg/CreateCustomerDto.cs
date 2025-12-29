using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.CustomerAgg
{
    public class CreateCustomerDto
    {
        public int CityId { get; set; }
        public int AppUserId { get; set; }

    }
}

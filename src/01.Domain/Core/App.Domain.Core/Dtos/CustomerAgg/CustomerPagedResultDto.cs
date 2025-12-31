using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.CustomerAgg
{
    public class CustomerPagedResultDto
    {
        public List<CustomerListDto> Customers { get; set; }
        public int TotalCount { get; set; }
    }
}

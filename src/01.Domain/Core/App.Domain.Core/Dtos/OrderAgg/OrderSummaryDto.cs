using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.OrderAgg
{
    public class OrderSummaryDto
    {
        public decimal BasePrice { get; set; }
        public string HomeServiceName { get; set; }
    }
}

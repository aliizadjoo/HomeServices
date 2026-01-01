using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.OrderAgg
{
    public class OrderPagedDtos
    {
        public List<OrderDto> orderDtos { get; set; } = [];
        public int TotalCount { get; set; }
    }
}

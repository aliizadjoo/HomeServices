using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.OrderAgg
{
    public class AvailableOrdersPagedDto
    {

        public List<AvailableOrderDto> AvailableOrdersDto { get; set; }=[];

        public int TotalCount { get; set; }
    }
}

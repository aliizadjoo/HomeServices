using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.HomeServiceAgg
{
    public class HomeservicePagedDto
    {
        public List<HomeserviceDto> HomeserviceDtos { get; set; } = [];
        public int TotalCount { get; set; }
    }
}

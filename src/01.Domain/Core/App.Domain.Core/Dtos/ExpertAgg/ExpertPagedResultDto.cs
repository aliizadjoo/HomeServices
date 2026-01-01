using App.Domain.Core.Dtos.CustomerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.ExpertAgg
{
    public class ExpertPagedResultDto
    {
        public List<ExpertListDto> Experts { get; set; } = [];
        public int TotalCount { get; set; }
    }
}

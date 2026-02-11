using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.ProposalAgg
{
    public class ProposalCreateDto
    {
        public int OrderId { get; set; }
        public int ExpertId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public DateTime SuggestedDate { get; set; }
        public TimeSpan ExecutionTime { get; set; }
       
    }
}

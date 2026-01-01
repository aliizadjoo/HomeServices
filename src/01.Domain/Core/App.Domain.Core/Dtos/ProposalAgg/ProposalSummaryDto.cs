using App.Domain.Core.Enums.ProposalAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.ProposalAgg
{
    public class ProposalSummaryDto
    {
        public int Id { get; set; }
        public int ExpertId { get; set; } 
        public string ExpertFirstName { get; set; }
        public string ExpertLastName { get; set; }
        public ProposalStatus Status { get; set; } = ProposalStatus.Pending;

        public decimal Price { get; set; }
        public string Description { get; set; }
      
    }
}

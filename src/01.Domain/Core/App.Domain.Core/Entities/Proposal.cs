using App.Domain.Core.Enums.ProposalAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class Proposal : BaseEntity
    {
        public decimal Price { get; set; }
        public string Description { get; set; }
        public ProposalStatus Status { get; set; } = ProposalStatus.Pending;

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ExpertId { get; set; }
        public Expert Expert { get; set; }
    }
}

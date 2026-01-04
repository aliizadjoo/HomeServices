using App.Domain.Core.Enums.ProposalAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.ProposalAgg
{

    public class ExpertProposalDto
    {
        public int ProposalId { get; set; }

        public DateTime ExecutionDate { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public int OrderId { get; set; }

        public string HomeServiceName { get; set; }

      
        public string CustomerFullName { get; set; }
      
    

        public decimal Price { get; set; } 

      
      

        public ProposalStatus Status { get; set; } 

        public string Description { get; set; }

        public string PersianExecutionDate { get; set; }
    }
}


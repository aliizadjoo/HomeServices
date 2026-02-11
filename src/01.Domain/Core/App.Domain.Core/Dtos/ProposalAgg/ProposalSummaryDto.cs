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

        public string? Bio { get; set; }
        public int Id { get; set; }
        public int ExpertId { get; set; }
        public double? AverageScore { get; set; }
        public string HomeServiceName { get; set; }
        public DateTime ExecutionDate { get; set; }
        public string PersianExecutionDate { get; set; }
        public string ExpertFirstName { get; set; }
        public string ExpertLastName { get; set; }
        public ProposalStatus Status { get; set; } = ProposalStatus.Pending;
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CityName { get; set; }

        public List<string> HomeserivceName { get; set; } = [];

    }
}

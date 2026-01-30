using App.Domain.Core.Dtos.ProposalAgg;
using App.Domain.Core.Enums.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.OrderAgg
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Description { get; set; }


        public OrderStatus Status { get; set; } = OrderStatus.WaitingForAdminApproval;

        public DateTime ExecutionDate { get; set; }
        public TimeSpan ExecutionTime { get; set; }

        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }

        public int HomeServiceId { get; set; }
        public string HomeServiceName { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }

        public List<string> ImagePaths { get; set; } = [];
        public List<ProposalSummaryDto> Proposals { get; set; } = [];
    }
}

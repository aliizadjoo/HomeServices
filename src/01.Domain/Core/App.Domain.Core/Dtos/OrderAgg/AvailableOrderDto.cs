using App.Domain.Core.Entities;
using App.Domain.Core.Enums.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.OrderAgg
{
    public class AvailableOrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.WaitingForAdminApproval;

        public bool IsProposalSent { get; set; }
        public string HomeServiceName { get; set; }


        public DateTime ExecutionDate { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public List<string> Images { get; set; } = [];

    }
}

using App.Domain.Core.Enums.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.WaitingForProposals;

      
        public DateTime ExecutionDate { get; set; }
        public TimeSpan ExecutionTime { get; set; }

        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int HomeServiceId { get; set; }
        public HomeService HomeService { get; set; }

        public Review? Review { get; set; }

        public List<Proposal> Proposals { get; set; } = [];
        public List<OrderImage> Images { get; set; } = [];
      
    }
}

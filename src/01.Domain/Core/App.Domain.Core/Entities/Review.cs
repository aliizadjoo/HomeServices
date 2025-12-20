using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class Review : BaseEntity
    {
        public string Comment { get; set; }
        public int Score { get; set; } 

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int ExpertId { get; set; }
        public Expert Expert { get; set; }
    }
}

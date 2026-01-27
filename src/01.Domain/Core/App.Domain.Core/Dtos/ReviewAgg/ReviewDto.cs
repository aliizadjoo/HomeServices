using App.Domain.Core.Entities;
using App.Domain.Core.Enums.ReviewAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.ReviewAgg
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }

        public int OrderId { get; set; }
        public string OrderDescription { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }

        public int ExpertId { get; set; }
        public string ExpertFirstName { get; set; }
        public string ExpertLastName { get; set; }
        public string? ImagePathExpert { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedAtShamsi { get; set; }

        public ReviewStatus ReviewStatus { get; set; }
    }
}

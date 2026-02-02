using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.ReviewAgg
{
    public class CreateReviewDto
    {
        public int Score { get; set; }
        public string Comment { get; set; }
        public int OrderId { get; set; }
        public int ExpertId { get; set; }
        public int CustomerId { get; set; }
       
    }
}

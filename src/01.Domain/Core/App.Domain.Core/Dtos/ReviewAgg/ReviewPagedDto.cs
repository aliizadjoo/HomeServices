using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.ReviewAgg
{
    public class ReviewPagedDto
    {
        public List<ReviewDto> ReviewDtos { get; set; } = [];
        public int TotalCount { get; set; }
    }
}

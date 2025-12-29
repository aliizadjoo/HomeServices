using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.ExpertAgg
{
    public class CreateExpertDto
    {
        public int CityId { get; set; }
        public int AppUserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class ExpertHomeService
    {
        public int ExpertId { get; set; }
        public Expert Expert { get; set; }

        public int HomeServiceId { get; set; }
        public HomeService HomeService { get; set; }
    }
}

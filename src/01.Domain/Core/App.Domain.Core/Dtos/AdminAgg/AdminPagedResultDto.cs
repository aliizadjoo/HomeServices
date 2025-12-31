using App.Domain.Core.Dtos.ExpertAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.AdminAgg
{
    public class AdminPagedResultDto
    {
        public List<AdminListDto> Admins { get; set; }
        public int TotalCount { get; set; }
    }
}

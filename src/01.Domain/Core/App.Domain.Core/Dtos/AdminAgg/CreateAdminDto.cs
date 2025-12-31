using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.AdminAgg
{
    public class CreateAdminDto
    {
        public int AppUserId { get; set; }
        public string StaffCode{ get; set; }
    }
}

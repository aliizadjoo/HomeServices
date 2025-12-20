using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class Admin : BaseEntity
    {
      
        public string StaffCode { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}

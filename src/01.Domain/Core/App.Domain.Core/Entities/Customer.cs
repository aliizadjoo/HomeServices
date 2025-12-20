using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string Address { get; set; }
       
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } 
    }
}

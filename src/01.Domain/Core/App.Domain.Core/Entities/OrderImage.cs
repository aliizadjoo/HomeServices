using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class OrderImage : BaseEntity
    {
        public string ImagePath { get; set; } 
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}

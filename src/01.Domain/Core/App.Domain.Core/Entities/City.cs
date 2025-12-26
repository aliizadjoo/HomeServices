using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public List<Expert> Experts { get; set; } = [];

        public List<Order> Orders { get; set; } = [];

        public List<Customer> Customers { get; set; } = [];

    }
}

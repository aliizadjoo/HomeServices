using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.AdminAgg
{
    public class AdminProfileDto
    {

        public int AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? ImagePath { get; set; }

        public string StaffCode { get; set; } 
        public decimal TotalRevenue { get; set; }
    }
}

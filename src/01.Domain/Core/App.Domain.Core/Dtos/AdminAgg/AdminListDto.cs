using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.AdminAgg
{
    public class AdminListDto
    {
        public int AdminId { get; set; }
        public int AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string StaffCode { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal TotalRevenue { get; set; }
        public DateTime CreatedAt { get; set; }
        

    }
}

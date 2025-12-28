using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class AppUser : IdentityUser<int>
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImagePath { get; set; }
        public Customer? CustomerProfile { get; set; }
        public Expert? ExpertProfile { get; set; }
        public Admin? AdminProfile { get; set; }
    }
}

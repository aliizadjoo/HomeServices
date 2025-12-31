using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.AccountAgg
{
    public class UserRegisterDto
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

   
        public string Email { get; set; }

        public string Password { get; set; }

    
        public int RoleId { get; set; }
        public string Role { get; set; }

        public int CityId { get; set; }

        public string? ImagePath { get; set; }

    }
}

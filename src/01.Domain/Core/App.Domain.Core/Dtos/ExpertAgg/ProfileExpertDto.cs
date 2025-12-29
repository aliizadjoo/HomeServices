using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.ExpertAgg
{
    public class ProfileExpertDto
    {
        public string Bio { get; set; }

        public decimal? WalletBalance { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImagePath { get; set; }

        public string CityName { get; set; }

        public List<string> HomeServices { get; set; } = [];
    }
}

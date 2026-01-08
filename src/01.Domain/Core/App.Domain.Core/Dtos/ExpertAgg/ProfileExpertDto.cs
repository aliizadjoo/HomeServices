using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.ExpertAgg
{
    public class ProfileExpertDto
    {
        public int AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
      
        public string? Bio { get; set; }
        public string? ImagePath { get; set; }
        public int CityId { get; set; }
        public List<int> HomeServicesId { get; set; } = [];

        public double? AverageScore { get; set; }
        public string? Email { get; set; }

        public List<string> HomeServices { get; set; } = [];
        public decimal? WalletBalance { get; set; }
        public string CityName { get; set; }

        public string? PhoneNumber { get; set; }

    }
}

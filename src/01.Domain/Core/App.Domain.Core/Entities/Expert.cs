using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class Expert : BaseEntity
    {
        public string Bio { get; set; }

        public decimal? WalletBalance { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
        public List<HomeService> HomeServices { get; set; } = [];
        public List<Proposal> Proposals { get; set; } = [];
        public List<Review> Reviews { get; set; } = [];
    }
}

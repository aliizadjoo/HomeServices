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
        public string? ProfilePicture { get; set; }

       
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

       
        public List<HomeService> Skills { get; set; } = [];
        public List<Proposal> Proposals { get; set; } = [];
        public List<Review> Reviews { get; set; } = [];
    }
}

using App.Domain.Core.Dtos.AdminAgg;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.UserAgg
{
    public class FilterUserDto
    {
        public FilterCustomerDto?  FilterCustomerDto { get; set; }
        public FilterExpertDto? FilterExpertDto { get; set; }
        public FilterAdminDto? FilterAdminDto { get; set; }
        
    }
}

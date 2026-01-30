using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Enums.OrderAgg
{
    public enum OrderStatus
    {
        WaitingForAdminApproval=0,
        WaitingForProposals = 1, 
        Started = 2,             
        Finished = 3,                       
        Cancelled = 4        
    }
}

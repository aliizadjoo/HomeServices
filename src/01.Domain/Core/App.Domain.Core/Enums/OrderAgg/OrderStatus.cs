using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Enums.OrderAgg
{
    public enum OrderStatus
    {
        WaitingForProposals = 1, 
        WaitingForSelection = 2, 
        ComingToPlace = 3,      
        Started = 4,             
        Finished = 5,                       
        Cancelled = 6        
    }
}

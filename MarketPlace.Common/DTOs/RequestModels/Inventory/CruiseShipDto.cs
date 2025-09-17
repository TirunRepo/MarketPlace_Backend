using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.RequestModels.Inventory
{
    public class CruiseShipDto
    {
        public int? CruiseShipId { get; set; }
        public string? ShipName { get; set; }
        public string? ShipCode { get; set; }
        public int CruiseLineId { get; set; }
        // Bind this for dropdowns or assignment
        public CruiseLineDto? CruiseLine { get; set; }

    }
}

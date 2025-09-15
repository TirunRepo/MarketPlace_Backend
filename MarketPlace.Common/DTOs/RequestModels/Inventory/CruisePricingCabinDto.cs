using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.RequestModels.Inventory
{
    public class CruisePricingCabinDto
    {
        public string CabinNo { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int CruisePricingInventoryId { get; set; }
    }
}

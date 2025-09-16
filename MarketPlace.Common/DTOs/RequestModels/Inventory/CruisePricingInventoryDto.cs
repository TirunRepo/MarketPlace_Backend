using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.RequestModels.Inventory
{
    public class CruisePricingInventoryDto
    {
        public int? CrusiePricingInventoryId { get; set; }
        public int? CruiseInventoryId { get; set; }
        public CruiseInventoryDto? CruiseInventory { get; set; }

        public DateTime? PriceValidFrom { get; set; }
        public DateTime? PriceValidTo { get; set; }

        public decimal? SinglePrice { get; set; }
        public decimal? DoublePrice { get; set; }
        public decimal? ThreeFourthPrice { get; set; }

        public decimal? Tax { get; set; }
        public decimal? Grats { get; set; }
        public decimal? NCCF { get; set; }

        public string? Category { get; set; }
        public string? CabinCategory { get; set; }
        public string? CabinNoType { get; set; }
        public string? CabinOccupancy { get; set; }

        // ✅ multiple cabins
        public virtual ICollection<CruisePricingCabinDto>? Cabins { get; set; }

        public bool EnableAgent { get; set; }
        public bool EnableAdmin { get; set; }
    }

}

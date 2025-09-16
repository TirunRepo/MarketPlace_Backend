using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.RequestModels.Inventory
{
    public class CruiseInventoryFormViewModel
    {
        public CruiseInventoryDto? CruiseInventoryDto { get; set; }
        public CruisePricingInventoryDto? cruisePricingInventoryDto { get; set; }

        // Prices
        public decimal SinglePrice { get; set; } = 0;
        public decimal DoublePrice { get; set; } = 0;
        public decimal ThreeFourthPrice { get; set; } = 0;
        public decimal NCCF { get; set; } = 0;
        public decimal Tax { get; set; } = 0;
        public decimal Grats { get; set; } = 0;

        public string? CabinCategory { get; set; }
        public string? Category { get; set; }

        // ✅ Accept multiple cabins
        public List<CruisePricingCabinDto>? Cabins { get; set; }

        public DateTime PriceValidFrom { get; set; } = DateTime.Today;
        public DateTime PriceValidTo { get; set; } = DateTime.Today.AddYears(1);

        // New fields
        public bool EnableAgent { get; set; } = true;
        public bool EnableAdmin { get; set; } = true;
    }
}

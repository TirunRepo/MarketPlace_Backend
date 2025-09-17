using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.RequestModels.Inventory
{
    public class CruiseInventoryRequest
    {
        // Basic Information
        public DateTime SailDate { get; set; }
        public string? GroupId { get; set; }
        public int Nights { get; set; }
        public string? PackageName { get; set; }

        // Cruise Details
        public string? DestinationId { get; set; }
        public string? DeparturePortId { get; set; }
        public string? CruiseLineId { get; set; }
        public string? ShipId { get; set; }

        // Pricing & Cabins
        public string? PricingType { get; set; }   // Net / Commissionable
        public string? Currency { get; set; }

        // Multiple cabins
        public List<CabinRequest>? Cabins { get; set; } = new();
    }

    public class CabinRequest
    {
        public string? Type { get; set; }       // GTY, etc.
        public string? CabinNo { get; set; }    // Cabin number
        public string? Status { get; set; }     // Available / Blocked, etc.
    }

}

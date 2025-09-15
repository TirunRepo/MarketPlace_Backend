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

        // These are required inputs
        public decimal SinglePrice { get; set; } = 0;
        public decimal DoublePrice { get; set; } = 0;
        public decimal ThreeFourthPrice { get; set; } = 0;
        public decimal NCCF { get; set; } = 0;
        public decimal Tax { get; set; } = 0;
        public decimal Grats { get; set; } = 0;
        public string? CabinCategory { get; set; }
        public string? CabinNo { get; set; }
        public string? Category { get; set; }

        public DateTime PriceValidFrom { get; set; } = DateTime.Today;
        public DateTime PriceValidTo { get; set; } = DateTime.Today.AddYears(1);

        // Dropdown list sources (don't decorate with [Required])
        //public DestinationDto? Destinations { get; set; }
        //public CruiseLineDto? CruiseLines { get; set; }
        //public DeparturePortDto? DeparturePorts { get; set; }
        //public CruiseShipDto? CruiseShips { get; set; }

        //public string? SelectedDestinationCode { get; set; }
        // public string? CruiselineId { get; set; }

        //public string? DestinationCode { get; set; }
        //public string? SelectedDeparturePortCode { get; set; }


        //public IEnumerable<SelectListItem> Currency { get; set; } = new List<SelectListItem>();
        //public IEnumerable<SelectListItem> CabinCategoryEnum { get; set; } = new List<SelectListItem>();
        //public IEnumerable<SelectListItem> CabinOccupancyEnum { get; set; } = new List<SelectListItem>();
        //public IEnumerable<SelectListItem> CabinNoTypeEnum { get; set; } = new List<SelectListItem>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.RequestModels.Inventory
{
    public class CruiseCabinDto
    {
        public int Id { get; set; }
        public int CruisePricingInventoryId { get; set; }
        public int CruiseInventoryId { get; set; }
        public DateTime Date { get; set; }
        public string SailDate { get; set; }
        public int? CruiseShipId { get; set; }
        public string GroupId { get; set; }
        public string CabinOccupancy { get; set; }
        public string CabinNumber { get; set; }
        public string CategoryId { get; set; }
        public string CabinStatus { get; set; }
        public string Stateroom { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}

using MarketPlace.Common.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.RequestModels.Inventory
{
    public class CruiseInventoryRequest :RecordBase
    {

        public int? Id { get; set; }
        /// <summary>
        /// Sail Date
        /// </summary>
        [Required]
        public DateTime SailDate { get; set; }
        /// <summary>
        /// Group Id
        /// </summary>
        public required  string GroupId { get; set; }
        /// <summary>
        /// Nights
        /// </summary>
        public required string Nights { get; set; }
        /// <summary>
        /// PackageName
        /// </summary>
        public required string PackageName { get; set; }

        public int DestinationId { get; set; }
        public int DeparturePortId { get; set; }
        public int CruiseLineId { get; set; }
        public int ShipId { get; set; }
        public required string ShipCode { get; set; }
        public required string CategoryId { get; set; }
        public required string Stateroom { get; set; }
        public required string CabinOccupancy { get; set; }

        public required string PricingType { get; set; }
        public int CommisionPercentage { get; set; } 
        public int SingleRate { get; set; }
        public int DoubleRate { get; set; }
        public int TripleRate { get; set; }
        public int Nccf { get; set; }
        public int Tax { get; set; }
        public int Grats { get; set; }
        public required string CurrenctType { get; set; }

        public bool EnableAdmin { get; set; }
        public bool EnableAgent { get; set; }

    }

}

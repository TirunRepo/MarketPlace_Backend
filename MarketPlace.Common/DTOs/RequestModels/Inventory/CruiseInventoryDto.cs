using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.RequestModels.Inventory
{
    public class CruiseInventoryDto
    {
        public int CruiseInventoryId { get; set; }
        [Display(Name = "Sail Date")]
        [DataType(DataType.Date)]
        public DateTime SailDate { get; set; }
        [Display(Name = "Group Id")]
        public string GroupId { get; set; }
        [Display(Name = "Package Name")]
        public string PackageDescription { get; set; }
        [Display(Name = "Nights")]
        public int Nights { get; set; }
        [Display(Name = "Category Id")]
        public string CategoryId { get; set; }
        public int? AgencyID { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        // Foreign Key values only (no navigation properties

        //public CruiseLineDto? CruiseLine { get; set; }
        public CruiseShipDto? CruiseShip { get; set; }
        public DestinationDto? Destination { get; set; }
        public DeparturePortDto? DeparturePort { get; set; }
    }
}

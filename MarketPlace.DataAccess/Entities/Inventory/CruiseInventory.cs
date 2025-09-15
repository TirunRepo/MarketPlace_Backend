using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Entities.Inventory
{
    public class CruiseInventory : EntityBase
    {
        [Key]
        public int CruiseInventoryId { get; set; }

        [Required]
        public DateTime SailDate { get; set; }
        [Required, StringLength(100)]
        public string GroupId { get; set; }
        [Required, StringLength(500)]
        public string PackageDescription { get; set; }
        [Required]
        public int Nights { get; set; }
        [Required]
        public int AgencyID { get; set; }
        // Foreign Key to CruiseShip  
        public int CruiseShipId { get; set; }  // 🔹 Matches PK in CruiseShip  
        [ForeignKey("CruiseShipId")]
        public virtual CruiseShip CruiseShip { get; set; }
        // Foreign Key to Destination  
        [Required]
        [StringLength(50)]
        public string DestinationCode { get; set; }  // 🔹 Updated to match PK in Destination  
        [ForeignKey("DestinationCode")]
        public virtual Destination Destination { get; set; }
        public int DeparturePortId { get; set; }  // Matches new PK in DeparturePort
        [ForeignKey("DeparturePortId")]
        public virtual DeparturePort DeparturePort { get; set; }
    }
}

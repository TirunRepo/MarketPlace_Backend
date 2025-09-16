using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Entities.Inventory
{
    [Table("CruisePricingCabin")]
    public class CruisePricingCabin
    {
        [Key]
        public int CruisePricingCabinId { get; set; }

        [Required]
        public int CruisePricingInventoryId { get; set; }

        [ForeignKey(nameof(CruisePricingInventoryId))]
        public virtual CruisePricingInventory CruisePricingInventory { get; set; }

        [Required]
        [StringLength(100)]
        public string CabinNo { get; set; }  // Renamed from CabinNo for clarity

        [Required]
        [StringLength(50)]
        public string Status { get; set; } // New field for storing status (Occupied, Vacant, Hold)
    }
}

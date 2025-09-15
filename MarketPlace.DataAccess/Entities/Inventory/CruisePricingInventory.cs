using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Entities.Inventory
{
    public class CruisePricingInventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CruisePricingInventoryId { get; set; }

        [Required]
        public int CruiseInventoryId { get; set; }

        [ForeignKey(nameof(CruiseInventoryId))]
        public virtual CruiseInventory CruiseInventory { get; set; }

        [Required]
        public DateTime PriceValidFrom { get; set; }

        [Required]
        public DateTime PriceValidTo { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal SinglePrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal DoublePrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? ThreeFourthPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Tax { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Grats { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal NCCF { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [StringLength(55)]
        public string CabinCategory { get; set; }

        [Required]
        public string CabinOccupancy { get; set; }

        // Navigation property
        public virtual ICollection<CruisePricingCabin> Cabins { get; set; }
    }
}

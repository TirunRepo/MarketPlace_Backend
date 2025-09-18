using MarketPlace.Common.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Entities.Inventory
{
    public class CruiseCabinPricingInventory
    {
        [Key]
        public int Id { get; set; }
        public int CruiseInventoryId { get; set; }
        public required string PricingType { get; set; }
        public int CommisionRate {get;set;}

        [Column(TypeName = "decimal(10,2)")]
        public decimal? SinglePrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? DoublePrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? TriplePrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Tax { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Grats { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Nccf { get; set; }
        [NotMapped]
        public List<CruiseCabinDetails> Cabins { get; set; }

        public RecordBase RecordBase { get; set; }
    }
}

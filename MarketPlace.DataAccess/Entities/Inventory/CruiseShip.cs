using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Entities.Inventory
{
    public class CruiseShip : EntityBase
    {
        [Key]
        public int CruiseShipId { get; set; }
        [Required]
        [StringLength(500)]
        public string ShipName { get; set; }
        [Required]
        [StringLength(50)]
        public string ShipCode { get; set; }

        [Required]
        public int CruiseLineId { get; set; }  // FK
        [ForeignKey("CruiseLineId")]
        public virtual CruiseLine CruiseLine { get; set; }
    }
}

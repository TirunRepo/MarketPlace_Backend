using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Entities.Inventory
{
    public class DeparturePort : EntityBase
    {
        [Key]
        public int DeparturePortId { get; set; }  // 🔹 New Primary Key

        [Required]
        [StringLength(50)]
        public string DeparturePortCode { get; set; }

        [Required]
        [StringLength(255)]
        public string DeparturePortName { get; set; }

        [Required]
        [StringLength(50)]
        public string DestinationCode { get; set; }  //  The actual FK property

        [ForeignKey("DestinationCode")]
        public virtual Destination Destination { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }

    }
}

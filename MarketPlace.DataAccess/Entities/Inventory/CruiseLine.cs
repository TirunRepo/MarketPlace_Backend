using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Entities.Inventory
{
    public class CruiseLine : EntityBase
    {
        [Key]
        public int CruiseLineId { get; set; }  // 🔹 New Primary Key
        [Required]
        [StringLength(500)]
        public string CruiseLineName { get; set; }
        [Required]
        [StringLength(50)]
        public string CruiseLineCode { get; set; }  // 🔹 Now a normal string field
    }
}

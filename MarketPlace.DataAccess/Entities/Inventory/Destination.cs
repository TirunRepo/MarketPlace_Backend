using MarketPlace.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Entities.Inventory
{
    public class Destination : EntityBase
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string DestinationCode { get; set; }  // 🔹 Primary Key

        [Required]
        [StringLength(255)]
        public string DestinationName { get; set; }
    }
}
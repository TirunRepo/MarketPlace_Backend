using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.RequestModels.Inventory
{
    public class CruiseLineDto
    {
        public int? CruiseLineId { get; set; }
        [Required]
        public string? CruiseLineName { get; set; }
        [Required]
        public string? CruiseLineCode { get; set; }
    }
}

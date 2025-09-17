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
        public int? CruiseLineId { get; set; } = null;
        public string? CruiseLineName { get; set; }
        public string? CruiseLineCode { get; set; }
    }
}

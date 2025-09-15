using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.RequestModels.Inventory
{
    public class DeparturePortDto
    {
        public int? DeparturePortId { get; set; }

        public string? DeparturePortCode { get; set; }

        public string? DeparturePortName { get; set; }
        //public string? Destination { get; set; }
        /// <summary>
        public string? DestinationCode { get; set; }
        /// </summary>
        //chnaged as it was giving error in controller file so shifted to string
        public DestinationDto? DestinationCodeObj { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }
    }
}

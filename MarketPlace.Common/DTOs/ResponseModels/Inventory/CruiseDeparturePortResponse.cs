using MarketPlace.Common.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.ResponseModels.Inventory
{
    public class CruiseDeparturePortResponse
    {

        public int? Id { get; set; }
        public required string Code { get; set; }

        public required string Name { get; set; }
        //public string? Destination { get; set; }
        /// <summary>
        public RecordBase? RecordBase { get; set; }

        public string? DestinationId { get; set; }  //  The actual FK property

    }
}

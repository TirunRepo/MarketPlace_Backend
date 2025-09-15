using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.RequestModels.Inventory
{
    public class SailDateDTO
    {
        public int SaleDateId { get; set; }
        public CruiseShipDto? CruiseShipDto { get; set; }
        public int? CruiseShipID { get; set; }
        public DateTime SailDateTime { get; set; }

        public Boolean IsActive { get; set; }
    }
}

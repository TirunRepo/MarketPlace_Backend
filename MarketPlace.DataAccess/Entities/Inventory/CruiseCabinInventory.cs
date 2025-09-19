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
    public class CruiseCabinInventory : RecordBase
    {
            [ForeignKey("CruiseInvemtoryId")]
            public int CruiseInventoryId { get; set; }

            [ForeignKey("CruisePricingInventoryId")]
            public int CruisePricingInventoryId { get; set; }

            [Key]
            public int Id { get; set; }
            public string CabinNo { get; set; }
            public string CabinType { get; set; }

            public string CabinOccupancy { get; set; }
    }
}

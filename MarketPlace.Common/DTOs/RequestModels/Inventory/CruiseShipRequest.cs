
using MarketPlace.Common.CommonModel;

namespace MarketPlace.Common.DTOs.RequestModels.Inventory
{
    public class CruiseShipRequest
    {
        public int? Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required int CruiseLineId { get; set; }

        public RecordBase? RecordBase { get; set; } 
    }
}

using MarketPlace.Business.Services.Interface.Inventory;
using MarketPlace.Common.APIResponse;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.DataAccess.Repositories.Inventory.Interface;

namespace MarketPlace.Business.Services.Services.Inventory
{
    public class CruiseInventoryService : ICruiseInventoryService
    {
        private readonly ICruiseInventoryRepository _cruiseInventoryRepository;

        public CruiseInventoryService(ICruiseInventoryRepository cruiseInventoryRepository)
        {
            _cruiseInventoryRepository = cruiseInventoryRepository ?? throw new ArgumentNullException(nameof(cruiseInventoryRepository));
        }


        public async Task<CruiseInventoryModel> Insert(CruiseInventoryRequest model) 
        {
            try
            {

                CruiseInventoryModel inventoryModel = new()
                {
                    SailDate = model.SailDate,
                    CabinOccupancy = model.CabinOccupancy,
                    CategoryId = model.CategoryId,
                    GroupId = model.GroupId,
                    Nights = model.Nights,
                    PackageName = model.PackageName,
                    ShipCode = model.ShipCode,
                    Stateroom = model.Stateroom,
                    CruiseLineId = model.CruiseLineId,
                    CruiseShipId = model.ShipId,
                    DeparturePortId = model.DeparturePortId,
                    DestinationId = model.DestinationId,
                    EnableAdmin = model.EnableAdmin,
                    EnableAgent = model.EnableAgent,
                };

                return await _cruiseInventoryRepository.Insert(inventoryModel);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

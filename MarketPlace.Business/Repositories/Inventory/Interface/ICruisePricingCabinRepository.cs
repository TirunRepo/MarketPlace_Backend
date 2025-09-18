using MarketPlace.Common.DTOs.RequestModels.Inventory;


namespace MarketPlace.DataAccess.Repositories.Inventory.Interface
{
    public interface ICruisePricingCabinRepository
    {
        Task<bool> InsertCabinsAsync(CruiseCabinPricingModel cabinDtos);
    }
}

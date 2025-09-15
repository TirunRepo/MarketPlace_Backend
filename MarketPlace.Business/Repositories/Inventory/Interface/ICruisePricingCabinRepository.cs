using MarketPlace.Common.DTOs.RequestModels.Inventory;


namespace MarketPlace.DataAccess.Repositories.Inventory.Interface
{
    public interface ICruisePricingCabinRepository
    {
        Task<IEnumerable<CruisePricingCabinDto>> GetAll(DateTime sailDate, int cruiseShipId, string groupId);

        Task<List<CruiseCabinDto>> GetCruiseCabinAsyn();
        Task UpdateCabinAsync(int id, string cabinNumber, string categoryId);
        Task InsertCabinsAsync(List<CruisePricingCabinDto> cabinDtos);
    }
}

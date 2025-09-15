using MarketPlace.Common.DTOs.RequestModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Interfaces.Inventory
{
    public interface ICruisePricingCabinService
    {
        Task<IEnumerable<CruisePricingCabinDto>> GetAll(DateTime sailDate, int cruiseShipId, string groupId);
        Task<List<CruiseCabinDto>> GetCruiseCabinAsyn();
        Task UpdateCabinAsync(int id, string cabinNumber, string categoryId);
        Task InsertCabinsAsync(List<CruisePricingCabinDto> cabinDtos);
    }
}

using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.DataAccess.Repositories.Inventory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Services.Inventory
{
    public class CruisePricingCabinService : ICruisePricingCabinService
    {
        private readonly ICruisePricingCabinRepository _CruisePricingCabinRepository;

        public CruisePricingCabinService(ICruisePricingCabinRepository CruisePricingCabinRepository)
        {
            _CruisePricingCabinRepository = CruisePricingCabinRepository ?? throw new ArgumentNullException(nameof(CruisePricingCabinRepository));
        }

        public async Task<IEnumerable<CruisePricingCabinDto>> GetAll(DateTime sailDate, int cruiseShipId, string groupId)
        {
            return await _CruisePricingCabinRepository.GetAll(sailDate, cruiseShipId, groupId);
        }

        public async Task<List<CruiseCabinDto>> GetCruiseCabinAsyn()
        {
            return await _CruisePricingCabinRepository.GetCruiseCabinAsyn();
        }

        public async Task UpdateCabinAsync(int id, string cabinNumber, string categoryId)
        {
            await _CruisePricingCabinRepository.UpdateCabinAsync(id, cabinNumber, categoryId);
        }

        public async Task InsertCabinsAsync(List<CruisePricingCabinDto> cabinDtos)
        {
            await _CruisePricingCabinRepository.InsertCabinsAsync(cabinDtos);
        }
    }
}

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
    public class CruisePricingInventoryService : ICruisePricingInventoryService
    {
        private readonly ICruisePricingInventoryRepository _CruisePricingRepository;

        public CruisePricingInventoryService(ICruisePricingInventoryRepository pricingRepository)
        {
            _CruisePricingRepository = pricingRepository ?? throw new ArgumentNullException(nameof(pricingRepository));
        }

        public async Task<IEnumerable<CruisePricingInventoryDto>> GetAll()
        {
            return await _CruisePricingRepository.GetAll();
        }

        public async Task<CruisePricingInventoryDto> GetById(int id)
        {
            return await _CruisePricingRepository.GetById(id);
        }

        public async Task<bool> Insert(CruisePricingInventoryDto dto)
        {
            return await _CruisePricingRepository.Insert(dto);
        }

        public async Task<CruisePricingInventoryDto> Update(CruisePricingInventoryDto dto)
        {
            return await _CruisePricingRepository.Update(dto);
        }

        public async Task<bool> Delete(int id)
        {
            return await _CruisePricingRepository.Delete(id);
        }
        public async Task<IEnumerable<CruisePricingInventoryDto>> GetByInventoryId(int inventoryId)
        {
            return await _CruisePricingRepository.GetByInventoryId(inventoryId);
        }

    }
}

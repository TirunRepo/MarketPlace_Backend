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
    public class CruiseInventoryService : IcruiseInventoryService
    {
        private readonly ICruiseInventoryRepository _cruiseInventoryRepository;

        public CruiseInventoryService(ICruiseInventoryRepository cruiseInventoryRepository)
        {
            _cruiseInventoryRepository = cruiseInventoryRepository ?? throw new ArgumentNullException(nameof(cruiseInventoryRepository));
        }

        public async Task<IEnumerable<CruiseInventoryDto>> GetAll()
        {
            return await _cruiseInventoryRepository.GetAll();
        }

        public async Task<CruiseInventoryDto> GetById(int id)
        {
            return await _cruiseInventoryRepository.GetById(id);
        }

        public async Task<CruiseInventoryDto> Insert(CruiseInventoryDto inventoryDto)
        {
            return await _cruiseInventoryRepository.Insert(inventoryDto);
        }

        public async Task<CruiseInventoryDto> Update(CruiseInventoryDto inventoryDto)
        {
            return await _cruiseInventoryRepository.Update(inventoryDto);
        }

        public async Task<bool> Delete(int id)
        {
            return await _cruiseInventoryRepository.Delete(id);
        }
    }
}

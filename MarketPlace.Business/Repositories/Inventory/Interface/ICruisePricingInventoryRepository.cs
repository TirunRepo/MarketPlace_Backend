using MarketPlace.Common.DTOs.RequestModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Repositories.Inventory.Interface
{
    public interface ICruisePricingInventoryRepository
    {
        Task<IEnumerable<CruisePricingInventoryDto>> GetAll();
        Task<CruisePricingInventoryDto> GetById(int id);
        Task<bool> Insert(CruisePricingInventoryDto dto);
        Task<CruisePricingInventoryDto> Update(CruisePricingInventoryDto dto);
        Task<IEnumerable<CruisePricingInventoryDto>> GetByInventoryId(int inventoryId);
        Task<bool> Delete(int id);
    }
}

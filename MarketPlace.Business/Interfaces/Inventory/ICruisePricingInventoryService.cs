using MarketPlace.Common.DTOs.RequestModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Interfaces.Inventory
{
    public interface ICruisePricingInventoryService
    {
        Task<IEnumerable<CruisePricingInventoryDto>> GetAll();
        Task<CruisePricingInventoryDto> GetById(int id);

        Task<IEnumerable<CruisePricingInventoryDto>> GetByInventoryId(int inventoryId);
        Task<bool> Insert(CruisePricingInventoryDto dto);
        Task<CruisePricingInventoryDto> Update(CruisePricingInventoryDto dto);
        Task<bool> Delete(int id);

    }
}

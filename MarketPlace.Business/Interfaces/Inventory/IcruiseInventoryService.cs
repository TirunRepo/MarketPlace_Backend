using MarketPlace.Common.DTOs.RequestModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Interfaces.Inventory
{
    public interface IcruiseInventoryService
    {
        Task<IEnumerable<CruiseInventoryDto>> GetAll();
        Task<CruiseInventoryDto> GetById(int id);
        Task<CruiseInventoryDto> Insert(CruiseInventoryDto InventoryDto);
        Task<CruiseInventoryDto> Update(CruiseInventoryDto InventoryDto);
        Task<bool> Delete(int id);
    }
}

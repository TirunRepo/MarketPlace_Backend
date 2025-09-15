using MarketPlace.Common.DTOs.RequestModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Repositories.Inventory.Interface
{
    public interface ICruiseInventoryRepository
    {
        Task<IEnumerable<CruiseInventoryDto>> GetAll();
        Task<CruiseInventoryDto> GetById(int id);
        Task<CruiseInventoryDto> Insert(CruiseInventoryDto cruiseInventoryDto);
        Task<CruiseInventoryDto> Update(CruiseInventoryDto cruiseInventoryDto);
        Task<bool> Delete(int id);
    }
}

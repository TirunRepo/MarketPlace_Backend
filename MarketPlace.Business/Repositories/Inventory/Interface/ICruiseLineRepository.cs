using MarketPlace.Common.DTOs.RequestModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Repositories.Inventory.Interface
{
    public interface ICruiseLineRepository
    {
        Task<CruiseLineDto> Insert(CruiseLineDto cruiseLineDto);
        Task<CruiseLineDto> Update(CruiseLineDto cruiseLineDto);
        Task<bool> Delete(int id);
        Task<IEnumerable<CruiseLineDto>> GetAll();
        Task<CruiseLineDto> GetById(int id);
    }
}

using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.DataAccess.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Repositories.Inventory.Interface
{
    public interface IcruiseShipRepository
    {
        Task<CruiseShipDto> Insert(CruiseShipDto cruiseShipDto);
        Task<CruiseShipDto> Update(CruiseShipDto cruiseShipDto);
        Task<bool> Delete(int id);
        Task<IEnumerable<CruiseShipDto>> GetAll();
        Task<CruiseShipDto> GetById(int id);
        Task<IEnumerable<CruiseShip>> GetByCruiseLineIdAsync(int cruiseLineId);
    }
}

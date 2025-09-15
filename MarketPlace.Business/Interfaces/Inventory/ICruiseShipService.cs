using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.DataAccess.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Interfaces.Inventory
{
    public interface ICruiseShipService
    {
        Task<IEnumerable<CruiseShipDto>> GetAll();
        Task<CruiseShipDto> GetById(int id);
        Task<CruiseShipDto> Insert(CruiseShipDto cruiseShipDto);
        Task<CruiseShipDto> Update(CruiseShipDto cruiseShipDto);
        Task<bool> Delete(int id);
        Task<IEnumerable<CruiseShip>> GetByCruiseLineIdAsync(int cruiseLineId);

    }
}

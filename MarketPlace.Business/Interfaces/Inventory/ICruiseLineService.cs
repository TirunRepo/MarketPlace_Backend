using MarketPlace.Common.DTOs.RequestModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Interfaces.Inventory
{
    public interface ICruiseLineService
    {
        Task<IEnumerable<CruiseLineDto>> GetAll();

        Task<CruiseLineDto> GetById(int id);
        Task<CruiseLineDto> Insert(CruiseLineDto cruiselineDto);
        Task<CruiseLineDto> Update(CruiseLineDto cruiselineDto); // <- This should return bool!
        Task<bool> Delete(int id);

    }
}

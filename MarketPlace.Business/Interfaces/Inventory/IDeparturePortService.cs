using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.DataAccess.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Interfaces.Inventory
{
    public interface IDeparturePortService
    {
        Task<IEnumerable<DeparturePortDto>> GetAll();
        Task<DeparturePortDto> GetById(int id);
        Task<DeparturePortDto> Insert(DeparturePortDto portDto);
        Task<DeparturePortDto> Update(DeparturePortDto portDto);
        Task<bool> Delete(int id);
        Task<IEnumerable<DeparturePort>> GetByDestinationCodeAsync(string destinationCode);
    }
}

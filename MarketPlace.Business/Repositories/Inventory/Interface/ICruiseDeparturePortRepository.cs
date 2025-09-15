using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.DataAccess.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Repositories.Inventory.Interface
{
    public interface ICruiseDeparturePortRepository
    {
        Task<DeparturePortDto> Insert(DeparturePortDto departurePortDto);
        Task<DeparturePortDto> Update(DeparturePortDto departurePortDto);
        Task<bool> Delete(int id);
        Task<IEnumerable<DeparturePortDto>> GetAll();
        Task<DeparturePortDto> GetById(int id);
        Task<IEnumerable<DeparturePort>> GetByDestinationCodeAsync(string destinationCode);
    }
}

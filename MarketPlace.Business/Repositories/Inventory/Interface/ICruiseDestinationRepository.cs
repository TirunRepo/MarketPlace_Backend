using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.DataAccess.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Repositories.Inventory.Interface
{
    public interface ICruiseDestinationRepository
    {
        Task<DestinationDto> Insert(DestinationDto destinationDto);
        Task<DestinationDto> Update(DestinationDto destinationDto);
        Task<bool> Delete(string destinationCode);
        Task<IEnumerable<DestinationDto>> GetAll();
        Task<DestinationDto> GetByCode(string destinationCode);

    }
}

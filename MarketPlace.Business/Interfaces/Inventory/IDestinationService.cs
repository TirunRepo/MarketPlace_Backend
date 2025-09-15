using MarketPlace.Common.DTOs.RequestModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Interfaces.Inventory
{
    public interface IDestinationService
    {
        Task<IEnumerable<DestinationDto>> GetAll();
        Task<DestinationDto> GetByCode(string code);
        Task<DestinationDto> Insert(DestinationDto destinationdto);
        Task<DestinationDto> Update(DestinationDto destinationdto);
        Task<bool> Delete(string code);
    }
}

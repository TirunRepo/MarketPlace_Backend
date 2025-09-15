using MarketPlace.Common.DTOs.RequestModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Interfaces.Inventory
{
    public interface ISailDateService
    {
        Task<SailDateDTO> Insert(SailDateDTO sailDateDTO);
        Task<SailDateDTO> Update(SailDateDTO sailDateDTO);
        Task<SailDateDTO> GetById(int SailDateID);
        Task<IEnumerable<SailDateDTO>> GetAll();
        Task<bool> Delete(int SailDateID);
    }
}

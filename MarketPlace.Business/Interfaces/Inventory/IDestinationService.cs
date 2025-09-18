using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.DTOs.ResponseModels.Inventory;
using MarketPlace.Common.PagedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Interfaces.Inventory
{
    public interface IDestinationService
    {
        Task<PagedData<DestinationResponse>> GetList();
        Task<DestinationResponse> GetById(int id);
        Task<DestinationRequest> Insert(DestinationRequest portDto);
        Task<DestinationRequest> Update(int Id, DestinationRequest portDto);
        Task<bool> Delete(int id);
    }
}

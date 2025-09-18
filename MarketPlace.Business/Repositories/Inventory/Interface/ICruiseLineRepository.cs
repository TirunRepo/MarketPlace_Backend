using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.DTOs.ResponseModels.Inventory;
using MarketPlace.Common.PagedData;
using MarketPlace.DataAccess.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Repositories.Inventory.Interface
{
    public interface ICruiseLineRepository
    {
        Task<CruiseLineRequest> Insert(CruiseLineRequest model);
        Task<CruiseLineRequest> Update(int Id, CruiseLineRequest model);
        Task<bool> Delete(int id);
        Task<PagedData<CruiseLineResponse>> GetList();
        Task<CruiseLineModal> GetById(int id);
    }
}

using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.DTOs.ResponseModels.Inventory;
using MarketPlace.Common.PagedData;
using MarketPlace.DataAccess.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Interfaces.Inventory
{
    public interface ICruiseLineService
    {

        Task<PagedData<CruiseLineResponse>> GetList();

        Task<CruiseLineModal> GetById(int id);
        Task<CruiseLineRequest> Insert(CruiseLineRequest cruiselineDto);
        Task<CruiseLineRequest> Update(int Id , CruiseLineRequest cruiselineDto); 
        Task<bool> Delete(int id);

    }
}

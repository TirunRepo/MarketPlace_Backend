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
    public interface IDeparturePortService
    {
        Task<PagedData<CruiseDeparturePortResponse>> GetList();
        Task<CruiseDeparturePortResponse> GetById(int id);
        Task<DeparturePortRequest> Insert(DeparturePortRequest portDto);
        Task<DeparturePortRequest> Update(int Id, DeparturePortRequest portDto);
        Task<bool> Delete(int id);

    }
}

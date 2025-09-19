using MarketPlace.Common.APIResponse;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Services.Interface.Inventory
{
    public interface ICruiseInventoryService
    {
        Task<CruiseInventoryModel> Insert(CruiseInventoryRequest model);
    }
}

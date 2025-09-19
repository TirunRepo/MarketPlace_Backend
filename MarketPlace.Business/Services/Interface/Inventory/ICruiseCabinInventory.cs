using MarketPlace.Common.DTOs.RequestModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Services.Interface.Inventory
{
    public interface ICruiseCabinInventory
    {
        Task<CruiseCabinInventoryRequest> Insert(CruiseCabinInventoryRequest model);

    }
}

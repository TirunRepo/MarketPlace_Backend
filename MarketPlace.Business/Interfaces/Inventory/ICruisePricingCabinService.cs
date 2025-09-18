using MarketPlace.Common.DTOs.RequestModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Interfaces.Inventory
{
    public interface ICruiseCabinPricingService
    {
        Task<bool> InsertCabinsAsync(CruiseInventoryRequest model);
    }
}

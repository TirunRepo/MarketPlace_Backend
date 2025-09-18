using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.DataAccess.Repositories.Inventory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Services.Inventory
{
    public class CruiseCabinPricingService : ICruiseCabinPricingService
    {
        private readonly ICruisePricingCabinRepository _CruisePricingCabinRepository;

        public CruiseCabinPricingService(ICruisePricingCabinRepository CruisePricingCabinRepository)
        {
            _CruisePricingCabinRepository = CruisePricingCabinRepository ?? throw new ArgumentNullException(nameof(CruisePricingCabinRepository));
        }

        public async Task<bool> InsertCabinsAsync(CruiseInventoryRequest model)
        {

            try
            {
                CruiseCabinPricingModel cruiseCabinPricingModel = new()
                {
                    PricingType = model.PricingType,
                    CommisionRate = model.CommisionPercentage,
                    SinglePrice = model.SingleRate,
                    DoublePrice = model.DoubleRate,
                    TripleRate = model.TripleRate,
                    Tax = model.Tax,
                    Nccf = model.Nccf,
                    Cabins = model.Cabins,
                    CruiseInventoryId = model.Id,
                    Grats = model.Grats
                };
                 return await _CruisePricingCabinRepository.InsertCabinsAsync(cruiseCabinPricingModel);
            }
            catch
            {

            }
            return false;
        }
    }
}

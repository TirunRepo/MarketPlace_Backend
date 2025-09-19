using AutoMapper;
using MarketPlace.Business.Services.Interface.Inventory;
using MarketPlace.Common.APIResponse;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.Types.Inventory;
using MarketPlace.DataAccess.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.API.Controllers.Inventory
{
    [ApiController]
    [Route("[controller]")]
    public class CruiseInventoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDeparturePortService _departurePortService;
        private readonly ICruiseShipService _cruiseShipService;
        private readonly ICruiseInventoryService _cruiseInventoryService;
        private readonly ICruiseLineService _cruiseLineService;
        private readonly IDestinationService _destinationService;
        private readonly ICruiseCabinPricingService _cruisePricingCabinService;
        private readonly AppDbContext _context;

        public CruiseInventoriesController(
            IDeparturePortService departurePortService,
            ICruiseShipService cruiseShipService,
            ICruiseInventoryService cruiseInventoryService,
            ICruiseLineService cruiseLineService,
            IDestinationService destinationService,
            ICruiseCabinPricingService cruisePricingCabinService,
            IMapper mapper,
            AppDbContext context)
        {
            _departurePortService = departurePortService;
            _cruiseShipService = cruiseShipService;
            _cruiseInventoryService = cruiseInventoryService;
            _cruiseLineService = cruiseLineService;
            _cruisePricingCabinService = cruisePricingCabinService;
            _destinationService = destinationService;
            _mapper = mapper;
            _context = context;
        }


        [HttpPost]
        public async Task<APIResponse<IActionResult>> SaveCruiseInventory([FromBody] CruiseInventoryRequest model)
        {

            try
            {
                // Insert cruise inventory
                var insertedInventory = await _cruiseInventoryService.Insert(model);
                if (insertedInventory == null) throw new Exception("Something went wrong please try again later.");

                // Save cabins if any
                //    if (model.Cabins != null && model.Cabins.Any())
                //    {
                //        var cabins = model.Cabins.Select(c => new CruisePricingCabinDto
                //        {
                //            CruisePricingInventoryId = insertedInventory.CruiseInventoryId, // corrected FK
                //            Type = c.Type,
                //            CabinNo = c.CabinNo,
                //            Status = c.Status
                //        }).ToList();

                //       // await _cruisePricingInventoryService.Insert(cabins);
                //    }

                //    return Ok(new { Success = true, Message = "Cruise inventory saved successfully." });
                //}
                //catch (Exception ex)
                //{
                //    return StatusCode(500, new
                //    {
                //        Success = false,
                //        Message = "Error saving cruise inventory.",
                //        Details = ex.Message
                //    });
            }
            catch
            {

            }
            return null;
        }



    }
}

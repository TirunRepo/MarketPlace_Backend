using AutoMapper;
using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Business.Services.Inventory;
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
    [Route("api/[controller]")]
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


    /*    // Existing GET endpoints (names purposely match frontend)
        [HttpGet("departures-by-destination/{destinationCode}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDeparturePortsByDestination(string destinationCode)
        {
            var departurePorts = await _departurePortService.GetByDestinationCodeAsync(destinationCode);
            return Ok(departurePorts.Select(p => new { p.Id, p.DeparturePortName }));
        }

        [HttpGet("ships-by-cruiseline/{cruiseLineId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetShipsByCruiseLine(int cruiseLineId)
        {
            var ships = await _cruiseShipService.GetByCruiseLineIdAsync(cruiseLineId);
            return Ok(ships.Select(s => new { s.Id, s.ShipName }));
        }*/


        [HttpGet("cabin-categories")]
        public ActionResult<IEnumerable<object>> GetCabinCategories()
        {
            var categories = Enum.GetValues(typeof(CabinCategoryEnum))
                .Cast<CabinCategoryEnum>()
                .Select(e => new { Value = (int)e, Text = e.ToString() })
                .ToList();

            return Ok(categories);
        }

        [HttpGet("cabin-number-types")]
        public ActionResult<IEnumerable<object>> GetCabinNumberTypes()
        {
            var types = Enum.GetValues(typeof(CabinNoTypeEnum))
                .Cast<CabinNoTypeEnum>()
                .Select(e => new { Value = (int)e, Text = e.ToString() })
                .ToList();

            return Ok(types);
        }

        [HttpGet("cabin-occupancies")]
        public ActionResult<IEnumerable<object>> GetCabinOccupancies()
        {
            var occupancies = Enum.GetValues(typeof(CabinOccupancyEnum))
                .Cast<CabinOccupancyEnum>()
                .Select(e => new { Value = (int)e, Text = e.ToString() })
                .ToList();

            return Ok(occupancies);
        }

        [HttpGet("cruiselines")]
        public async Task<IActionResult> GetCruiseLines()
        {
            var cruiseLines = await _cruiseLineService.GetList();
            return Ok(cruiseLines.Items.Select(cl => new
            {
                cl.Id,
                cl.Name,
                cl.Code
            }));
        }

        [HttpGet("destinations")]
        public async Task<IActionResult> GetDestinations()
        {
            var destinations = await _destinationService.GetList();
            return Ok(destinations.Items.Select(d => new
            {
                d.Id,
                d.Code,
                d.Name
            }));
        }        
    }
}

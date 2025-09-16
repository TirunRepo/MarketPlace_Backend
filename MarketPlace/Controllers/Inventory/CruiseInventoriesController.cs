using AutoMapper;
using MarketPlace.Business.Interfaces.Inventory;
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
        private readonly IcruiseInventoryService _cruiseInventoryService;
        private readonly ICruiseLineService _cruiseLineService;
        private readonly IDestinationService _destinationService;
        private readonly ICruisePricingInventoryService _cruisePricingInventoryService;
        private readonly ISailDateService _sailDateService;
        private readonly ICruisePricingCabinService _cruisePricingCabinService;
        private readonly AppDbContext _context;

        public CruiseInventoriesController(
            IDeparturePortService departurePortService,
            ISailDateService sailDateService,
            ICruiseShipService cruiseShipService,
            IcruiseInventoryService cruiseInventoryService,
            ICruiseLineService cruiseLineService,
            IDestinationService destinationService,
            ICruisePricingInventoryService cruisePricingInventoryService,
            ICruisePricingCabinService cruisePricingCabinService,
            IMapper mapper,
            AppDbContext context)
        {
            _departurePortService = departurePortService;
            _cruiseShipService = cruiseShipService;
            _cruiseInventoryService = cruiseInventoryService;
            _cruiseLineService = cruiseLineService;
            _destinationService = destinationService;
            _cruisePricingInventoryService = cruisePricingInventoryService;
            _sailDateService = sailDateService;
            _cruisePricingCabinService = cruisePricingCabinService;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("occupancies")]
        public async Task<ActionResult<IEnumerable<SelectListItem>>> GetOccupancies(int sailDateId, string groupId, string category)
        {
            var sailDate = await GetSailDateByIdAsync(sailDateId);
            if (sailDate is null) return NotFound();

            var result = (await _cruisePricingInventoryService.GetAll())
                .Where(x => x.CruiseInventory.GroupId == groupId
                         && x.CruiseInventory.SailDate.Date == sailDate.SailDateTime.Date
                         && x.CruiseInventory.CruiseShip.CruiseShipId == sailDate.CruiseShipID
                         && x.Category == category)
                .Select(x => x.CabinOccupancy)
                .Distinct()
                .OrderBy(x => x)
                .Select(o => new SelectListItem
                {
                    Value = o,
                    Text = Enum.GetName(typeof(CabinOccupancyEnum), int.Parse(o)) ?? o
                })
                .ToList();

            return Ok(result);
        }

        [HttpGet("staterooms")]
        public async Task<ActionResult<IEnumerable<SelectListItem>>> GetStaterooms(int sailDateId, string groupId, string category, string occupancy)
        {
            var sailDate = await GetSailDateByIdAsync(sailDateId);
            if (sailDate is null) return NotFound();

            var result = (await _cruisePricingInventoryService.GetAll())
                .Where(x => x.CruiseInventory.GroupId == groupId
                         && x.CruiseInventory.SailDate.Date == sailDate.SailDateTime.Date
                         && x.CruiseInventory.CruiseShip.CruiseShipId == sailDate.CruiseShipID
                         && x.Category == category
                         && x.CabinOccupancy == occupancy)
                .Select(x => x.CabinCategory)
                .Distinct()
                .OrderBy(x => x)
                .Select(c => new SelectListItem
                {
                    Value = c,
                    Text = Enum.GetName(typeof(CabinCategoryEnum), int.Parse(c)) ?? c
                })
                .ToList();

            return Ok(result);
        }

        [HttpPost("update-cabin")]
        public async Task<IActionResult> UpdateCabin([FromBody] CruiseCabinDto model)
        {
            await _cruisePricingCabinService.UpdateCabinAsync(model.Id, model.CabinNumber, model.CategoryId);
            return Ok();
        }

        [HttpPost("add-cabins")]
        public async Task<IActionResult> AddCabins([FromBody] CruiseCabinDto model)
        {
            var cabins = await _cruisePricingCabinService.GetCruiseCabinAsyn();
            var sailDate = await GetSailDateByIdAsync(int.Parse(model.SailDate));
            if (sailDate is null) return NotFound("SailDate not found.");

            var matching = cabins
                .Where(x => x.GroupId == model.GroupId
                         && x.Date.Date == sailDate.SailDateTime.Date
                         && x.CruiseShipId == sailDate.CruiseShipID
                         && x.CategoryId == model.CategoryId
                         && x.CabinOccupancy == ((CabinOccupancyEnum)int.Parse(model.CabinOccupancy)).ToString()
                         && x.Stateroom == ((CabinCategoryEnum)int.Parse(model.Stateroom)).ToString())
                .ToList();

            var cabinDetails = GenerateCabins(model, matching);
            await _cruisePricingCabinService.InsertCabinsAsync(cabinDetails);
            return Ok();
        }

        private static List<CruisePricingCabinDto> GenerateCabins(CruiseCabinDto cabin, List<CruiseCabinDto> existing)
        {
            var cabins = new List<CruisePricingCabinDto>();
            int cabinIndex = 1;

            if (cabin.Type.ToLower() == "gty" && int.TryParse(cabin.CabinNumber, out int total))
            {
                foreach (var _ in Enumerable.Range(1, total))
                {
                    while (existing.Any(x => x.CabinNumber == $"GTY{cabinIndex}"))
                        cabinIndex++;

                    cabins.Add(new CruisePricingCabinDto
                    {
                        CabinNo = $"GTY{cabinIndex++}",
                        Status = cabin.Status,
                        Type = cabin.Type,
                        CruisePricingInventoryId = existing.FirstOrDefault()?.CruisePricingInventoryId ?? 0
                    });
                }
            }
            else
            {
                cabins.Add(new CruisePricingCabinDto
                {
                    CabinNo = cabin.CabinNumber,
                    Status = cabin.Status,
                    Type = cabin.Type,
                    CruisePricingInventoryId = existing.FirstOrDefault()?.CruisePricingInventoryId ?? 0
                });
            }
            return cabins;
        }

        [HttpGet("departures-by-destination/{destinationCode}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDeparturePortsByDestination(string destinationCode)
        {
            var departurePorts = await _departurePortService.GetByDestinationCodeAsync(destinationCode);
            return Ok(departurePorts.Select(p => new { p.DeparturePortId, p.DeparturePortName }));
        }

        [HttpGet("ships-by-cruiseline/{cruiseLineId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetShipsByCruiseLine(int cruiseLineId)
        {
            var ships = await _cruiseShipService.GetByCruiseLineIdAsync(cruiseLineId);
            return Ok(ships.Select(s => new { s.CruiseShipId, s.ShipName }));
        }

        [HttpGet("saildates-by-ship/{cruiseShipId}")]
        public async Task<ActionResult<IEnumerable<SelectListItem>>> GetSailDatesByCruiseShipId(int cruiseShipId)
        {
            var sailDates = await _sailDateService.GetAll();
            var result = sailDates
                .Where(s => s.CruiseShipID == cruiseShipId)
                .Select(s => new SelectListItem
                {
                    Value = s.SaleDateId.ToString(),
                    Text = s.SailDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                })
                .ToList();

            return Ok(result);
        }

        // Dropdown enum endpoints follow the same pattern:
        // Example for Cabin Categories:
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
            var cruiseLines = await _cruiseLineService.GetAll();
            return Ok(cruiseLines.Select(cl => new
            {
                cl.CruiseLineId,
                cl.CruiseLineName
            }));
        }

        [HttpGet("destinations")]
        public async Task<IActionResult> GetDestinations()
        {
            var destinations = await _destinationService.GetAll();
            return Ok(destinations.Select(d => new
            {
                d.DestinationCode,
                d.DestinationName
            }));
        }

        [HttpGet("cruiseships/{cruiseLineId}")]
        public async Task<IActionResult> GetCruiseShipsByCruiseLine(int cruiseLineId)
        {
            var cruiseShips = await _context.CruiseShips
                .Where(cs => cs.CruiseLineId == cruiseLineId)
                .Select(cs => new
                {
                    cs.CruiseShipId,
                    cs.ShipName
                })
                .ToListAsync();

            return Ok(cruiseShips);
        }

        // NEW METHODS TO ADD

        
    }
}

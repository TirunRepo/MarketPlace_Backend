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


        [HttpPost]
        public async Task<IActionResult> SaveCruiseInventory([FromBody] CruiseInventoryFormViewModel model)
        {
            if (model == null || model.CruiseInventoryDto == null)
                return BadRequest(new { Success = false, Message = "Inventory data is missing." });

            try
            {
                model.CruiseInventoryDto.CreatedOn = DateTime.UtcNow;
                model.CruiseInventoryDto.CreatedBy = 1; // replace with actual user id

                var cruiseInventory = await _cruiseInventoryService.Insert(model.CruiseInventoryDto);

                var cabins = await GenerateCabins(model);

                var pricing = new CruisePricingInventoryDto
                {
                    CruiseInventoryId = cruiseInventory.CruiseInventoryId,
                    CabinCategory = model.CabinCategory,
                    Category = model.CruiseInventoryDto.CategoryId,
                    CabinOccupancy = model.cruisePricingInventoryDto?.CabinOccupancy,
                    CabinNoType = model.cruisePricingInventoryDto?.CabinNoType,
                    SinglePrice = model.SinglePrice,
                    DoublePrice = model.DoublePrice,
                    ThreeFourthPrice = model.ThreeFourthPrice,
                    NCCF = model.NCCF,
                    Tax = model.Tax,
                    Grats = model.Grats,
                    PriceValidFrom = model.PriceValidFrom,
                    PriceValidTo = model.PriceValidTo,
                    EnableAgent = model.EnableAgent,
                    EnableAdmin = model.EnableAdmin,
                    Cabins = cabins
                };

                await _cruisePricingInventoryService.Insert(pricing);

                return Ok(new { Success = true, Message = "Cruise inventory saved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Error saving cruise inventory.", Details = ex.Message });
            }
        }
        /// <summary>
        /// Get paginated list of cruise inventories
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CruiseInventoryDto>>> GetList(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] int? cruiseLineId = null)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _context.CruiseInventories.AsQueryable();

            // Optional filtering
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.CruiseShip.ShipName.Contains(search)
                                      || x.CruiseShip.ShipCode.Contains(search));
            }

            if (cruiseLineId != 0 && cruiseLineId != null)
            {
                query = query.Where(x => x.CruiseShip.CruiseLineId == cruiseLineId);
            }

            // Count total records
            var totalRecords = await query.CountAsync();

            // Paging
            var inventories = await query
                .OrderByDescending(x => x.CreatedOn)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = _mapper.Map<IEnumerable<CruiseInventoryDto>>(inventories);

            return Ok(new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = response
            });
        }

        // Existing GET endpoints (names purposely match frontend)
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
                cl.CruiseLineName,
                cl.CruiseLineCode
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


        // ✅ GTY + Explicit cabin generation logic
        private async Task<List<CruisePricingCabinDto>> GenerateCabins(CruiseInventoryFormViewModel model)
        {
            var existingCabins = await _cruisePricingCabinService.GetAll(
                model.CruiseInventoryDto.SailDate,
                model.CruiseInventoryDto.CruiseShip?.CruiseShipId ?? 0,
                model.CruiseInventoryDto.GroupId
            );

            List<CruisePricingCabinDto> cabins = new List<CruisePricingCabinDto>();
            int cabinIndex = 1;

            if (model.Cabins != null)
            {
                foreach (var cabin in model.Cabins)
                {
                    int gtyCount;

                    if (cabin.Type == "Gty" && int.TryParse(cabin.CabinNo, out gtyCount))
                    {
                        for (int i = 1; i <= gtyCount; i++)
                        {
                            bool isRecordExist = true;

                            while (isRecordExist)
                            {
                                var generatedNo = "GTY" + cabinIndex.ToString();
                                isRecordExist = existingCabins.Any(x => x.CabinNo == generatedNo);

                                if (!isRecordExist)
                                {
                                    cabins.Add(new CruisePricingCabinDto
                                    {
                                        CabinNo = generatedNo,
                                        Status = cabin.Status,
                                        Type = "Gty"
                                    });
                                }

                                cabinIndex++;
                            }
                        }
                    }
                    else
                    {
                        cabins.Add(new CruisePricingCabinDto
                        {
                            CabinNo = cabin.CabinNo,
                            Status = cabin.Status,
                            Type = cabin.Type
                        });
                    }
                }
            }

            return cabins;
        }



        // NEW METHODS TO ADD

        
    }
}

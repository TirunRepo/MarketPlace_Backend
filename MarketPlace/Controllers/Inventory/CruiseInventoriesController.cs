using AutoMapper;
using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.Types.Inventory;
using MarketPlace.DataAccess.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.ComponentModel;
using System.Text.Json;

namespace Marketplace.API.Controllers.Inventory
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class CruiseInventoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDeparturePortService _departurePortService;
        private readonly ICruiseShipService _cruiseShipService;
        private readonly IcruiseInventoryService _cruiseInventories;
        private readonly ICruiseLineService _cruiseLineService;
        private readonly IDestinationService _destinationService;
        private readonly ICruisePricingInventoryService _cruisePricingInventoryService;
        private readonly ISailDateService _sailDateService;
        private readonly ICruisePricingCabinService _cruisePricingCabinService;

        public CruiseInventoriesController(
            AppDbContext context,
            IDeparturePortService departurePortService,
            ISailDateService sailDateService,
            ICruiseShipService cruiseShipService,
            IcruiseInventoryService cruiseInventories,
            ICruiseLineService cruiseLineService,
            IDestinationService destinationService,
            ICruisePricingInventoryService cruisePricingInventoryService,
            ICruisePricingCabinService cruisePricingCabinService,
            IMapper mapper)
        {
            _context = context;
            _departurePortService = departurePortService;
            _cruiseShipService = cruiseShipService;
            _cruiseInventories = cruiseInventories;
            _cruiseLineService = cruiseLineService;
            _destinationService = destinationService;
            _cruisePricingInventoryService = cruisePricingInventoryService;
            _sailDateService = sailDateService;
            _cruisePricingCabinService = cruisePricingCabinService;
            _mapper = mapper;
        }

        // GET: api/Admin/CruiseInventories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventories = await _cruiseInventories.GetAll();
            return Ok(inventories);
        }

        [HttpGet("SailDates")]
        public async Task<IActionResult> GetSailDates()
        {
            var sailDates = await _sailDateService.GetAll();
            var result = sailDates
                .OrderBy(x => x.SailDateTime)
                .ThenBy(x => x.CruiseShipDto?.ShipCode)
                .Select(s => new
                {
                    s.SaleDateId,
                    Text = (!string.IsNullOrEmpty(s.CruiseShipDto?.ShipCode) ? s.CruiseShipDto.ShipCode + "-" : "") + s.SailDateTime.ToString("ddMMMyyyy")
                });
            return Ok(result);
        }

        [HttpGet("Groups/{sailDateId}")]
        public async Task<IActionResult> GetGroups(int sailDateId)
        {
            var sailDate = (await _sailDateService.GetAll()).FirstOrDefault(x => x.SaleDateId == sailDateId);
            if (sailDate == null) return NotFound();

            var groups = (await _cruiseInventories.GetAll())
                .Where(x => x.SailDate.Date == sailDate.SailDateTime.Date && x.CruiseShip.CruiseShipId == sailDate.CruiseShipID)
                .Select(x => x.GroupId)
                .Distinct()
                .OrderBy(g => g);

            return Ok(groups);
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories(int sailDateId, string groupId)
        {
            var sailDate = (await _sailDateService.GetAll()).FirstOrDefault(x => x.SaleDateId == sailDateId);
            if (sailDate == null) return NotFound();

            var categories = (await _cruisePricingInventoryService.GetAll())
                .Where(x => x.CruiseInventory.GroupId == groupId &&
                            x.CruiseInventory.SailDate.Date == sailDate.SailDateTime.Date &&
                            x.CruiseInventory.CruiseShip.CruiseShipId == sailDate.CruiseShipID)
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(c => c);

            return Ok(categories);
        }

        [HttpGet("Occupancies")]
        public async Task<IActionResult> GetOccupancies(int sailDateId, string groupId, string category)
        {
            var sailDate = (await _sailDateService.GetAll()).FirstOrDefault(x => x.SaleDateId == sailDateId);
            if (sailDate == null) return NotFound();

            var occupancies = (await _cruisePricingInventoryService.GetAll())
                .Where(x => x.CruiseInventory.GroupId == groupId &&
                            x.CruiseInventory.SailDate.Date == sailDate.SailDateTime.Date &&
                            x.CruiseInventory.CruiseShip.CruiseShipId == sailDate.CruiseShipID &&
                            x.Category == category)
                .Select(x => x.CabinOccupancy)
                .Distinct()
                .OrderBy(o => o);

            return Ok(occupancies);
        }

        [HttpGet("Staterooms")]
        public async Task<IActionResult> GetStaterooms(int sailDateId, string groupId, string category, string occupancy)
        {
            var sailDate = (await _sailDateService.GetAll()).FirstOrDefault(x => x.SaleDateId == sailDateId);
            if (sailDate == null) return NotFound();

            var staterooms = (await _cruisePricingInventoryService.GetAll())
                .Where(x => x.CruiseInventory.GroupId == groupId &&
                            x.CruiseInventory.SailDate.Date == sailDate.SailDateTime.Date &&
                            x.CruiseInventory.CruiseShip.CruiseShipId == sailDate.CruiseShipID &&
                            x.Category == category &&
                            x.CabinOccupancy == occupancy)
                .Select(x => x.CabinCategory)
                .Distinct()
                .OrderBy(s => s);

            return Ok(staterooms);
        }

        [HttpGet("FilterCabins")]
        public async Task<IActionResult> FilterCabins(int sailDateId, string groupId, string category, string occupancy, string cabinCategory)
        {
            var sailDate = (await _sailDateService.GetAll()).FirstOrDefault(x => x.SaleDateId == sailDateId);
            if (sailDate == null) return NotFound();

            var cabins = await _cruisePricingCabinService.GetCruiseCabinAsyn();
            var filtered = cabins.Where(x =>
                x.GroupId == groupId &&
                x.Date.Date == sailDate.SailDateTime.Date &&
                x.CruiseShipId == sailDate.CruiseShipID &&
                x.CategoryId == category &&
                x.CabinOccupancy == ((CabinOccupancyEnum)int.Parse(occupancy)).ToString() &&
                x.Stateroom == ((CabinCategoryEnum)int.Parse(cabinCategory)).ToString())
                .Select(x => new
                {
                    x.Id,
                    x.CabinNumber,
                    x.CabinOccupancy,
                    x.CabinStatus,
                    x.CategoryId,
                    x.CruiseInventoryId,
                    x.CruisePricingInventoryId,
                    x.CruiseShipId,
                    x.GroupId,
                    x.Stateroom,
                    SailDate = (!string.IsNullOrEmpty(sailDate.CruiseShipDto?.ShipCode) ? sailDate.CruiseShipDto.ShipCode + "-" : "") + sailDate.SailDateTime.ToString("ddMMMyyyy")
                });

            return Ok(filtered);
        }

        [HttpPost("UpdateCabin")]
        public async Task<IActionResult> UpdateCabin([FromBody] CruiseCabinDto model)
        {
            await _cruisePricingCabinService.UpdateCabinAsync(model.Id, model.CabinNumber, model.CategoryId);
            return Ok(true);
        }

        [HttpPost("AddCabins")]
        public async Task<IActionResult> AddCabins([FromBody] CruiseCabinDto model)
        {
            var cabins = await _cruisePricingCabinService.GetCruiseCabinAsyn();
            var sailDate = (await _sailDateService.GetAll()).FirstOrDefault(x => x.SaleDateId == int.Parse(model.SailDate));
            var data = cabins.Where(x =>
                x.GroupId == model.GroupId &&
                x.Date.Date == sailDate.SailDateTime.Date &&
                x.CruiseShipId == sailDate.CruiseShipID &&
                x.CategoryId == model.CategoryId &&
                x.CabinOccupancy == ((CabinOccupancyEnum)int.Parse(model.CabinOccupancy)).ToString() &&
                x.Stateroom == ((CabinCategoryEnum)int.Parse(model.Stateroom)).ToString()).ToList();

            var cabinDetails = await GetCruisePricingCabins(model, data);
            await _cruisePricingCabinService.InsertCabinsAsync(cabinDetails);

            return Ok(true);
        }

        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("File is empty");

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var stream = file.OpenReadStream();
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null) return BadRequest("No worksheet found");

            int rowCount = worksheet.Dimension.Rows;
            var cruiseInventoryFormViewModels = new List<CruiseInventoryFormViewModel>();

            for (int row = 2; row <= rowCount; row++)
            {
                var model = new CruiseInventoryFormViewModel
                {
                    CruiseInventoryDto = new CruiseInventoryDto
                    {
                        GroupId = worksheet.Cells[row, 1].Text,
                        PackageDescription = worksheet.Cells[row, 2].Text,
                        Nights = int.Parse(worksheet.Cells[row, 3].Text),
                        AgencyID = !string.IsNullOrEmpty(worksheet.Cells[row, 4].Text) ? int.Parse(worksheet.Cells[row, 4].Text) : null,
                        Destination = new DestinationDto { DestinationName = worksheet.Cells[row, 5].Text },
                        DeparturePort = new DeparturePortDto { DeparturePortName = worksheet.Cells[row, 6].Text },
                        CruiseShip = new CruiseShipDto { ShipName = worksheet.Cells[row, 7].Text },
                        SailDate = DateTime.Parse(worksheet.Cells[row, 8].Text)
                    },
                    cruisePricingInventoryDto = new CruisePricingInventoryDto
                    {
                        SinglePrice = decimal.TryParse(worksheet.Cells[row, 9].Text, out var sp) ? sp : null,
                        DoublePrice = decimal.TryParse(worksheet.Cells[row, 10].Text, out var dp) ? dp : null,
                        ThreeFourthPrice = decimal.TryParse(worksheet.Cells[row, 11].Text, out var tdp) ? tdp : null,
                        NCCF = decimal.TryParse(worksheet.Cells[row, 12].Text, out var nccf) ? nccf : null,
                        Tax = decimal.TryParse(worksheet.Cells[row, 13].Text, out var tax) ? tax : null,
                        Grats = decimal.TryParse(worksheet.Cells[row, 14].Text, out var grats) ? grats : null,
                        CabinCategory = worksheet.Cells[row, 15].Text,
                        CabinNo = worksheet.Cells[row, 16].Text,
                        Category = worksheet.Cells[row, 17].Text,
                        PriceValidFrom = DateTime.TryParse(worksheet.Cells[row, 18].Text, out var from) ? from : null,
                        PriceValidTo = DateTime.TryParse(worksheet.Cells[row, 19].Text, out var to) ? to : null,
                        CabinOccupancy = worksheet.Cells[row, 20].Text,
                        CabinNoType = worksheet.Cells[row, 21].Text
                    }
                };
                cruiseInventoryFormViewModels.Add(model);
            }

            // Map destinations, ports, ships
            var destinations = await _destinationService.GetAll();
            var ports = await _departurePortService.GetAll();
            var ships = await _cruiseShipService.GetAll();

            foreach (var item in cruiseInventoryFormViewModels)
            {
                if (!string.IsNullOrEmpty(item.CruiseInventoryDto.Destination.DestinationName))
                {
                    var dest = destinations.FirstOrDefault(x => x.DestinationName.ToLower().Contains(item.CruiseInventoryDto.Destination.DestinationName.ToLower()));
                    if (dest != null) item.CruiseInventoryDto.Destination = dest;
                }

                if (!string.IsNullOrEmpty(item.CruiseInventoryDto.DeparturePort.DeparturePortName))
                {
                    var port = ports.FirstOrDefault(x => x.DeparturePortName.ToLower().Contains(item.CruiseInventoryDto.DeparturePort.DeparturePortName.ToLower()));
                    if (port != null) item.CruiseInventoryDto.DeparturePort = port;
                }

                if (!string.IsNullOrEmpty(item.CruiseInventoryDto.CruiseShip.ShipName))
                {
                    var ship = ships.FirstOrDefault(x => x.ShipName.ToLower().Contains(item.CruiseInventoryDto.CruiseShip.ShipName.ToLower()));
                    if (ship != null) item.CruiseInventoryDto.CruiseShip = ship;
                }

                item.CruiseInventoryDto.CreatedOn = DateTime.Now;
                item.CruiseInventoryDto.CreatedBy = 1;

                var cruiseInventory = await _cruiseInventories.Insert(item.CruiseInventoryDto);
                item.cruisePricingInventoryDto.CruiseInventoryId = cruiseInventory.CruiseInventoryId;
                await _cruisePricingInventoryService.Insert(item.cruisePricingInventoryDto);
            }

            return Ok(new { Message = "All rows saved successfully" });
        }

        // Helper to create CruisePricingCabins
        private async Task<List<CruisePricingCabinDto>> GetCruisePricingCabins(CruiseCabinDto cabin, List<CruiseCabinDto> data)
        {
            List<CruisePricingCabinDto> cabins = new List<CruisePricingCabinDto>();
            int cabinIndex = 1;

            if (cabin.Type == "Gty" && int.TryParse(cabin.CabinNumber, out int cabinNo))
            {
                for (int i = 1; i <= cabinNo; i++)
                {
                    bool exists = true;
                    while (exists)
                    {
                        exists = data.Any(x => x.CabinNumber == "GTY" + cabinIndex);
                        if (!exists)
                        {
                            cabins.Add(new CruisePricingCabinDto
                            {
                                CabinNo = "GTY" + cabinIndex,
                                Status = cabin.Status,
                                Type = cabin.Type,
                                CruisePricingInventoryId = data[0].CruisePricingInventoryId
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
                    CabinNo = cabin.CabinNumber,
                    Status = cabin.Status,
                    Type = cabin.Type,
                    CruisePricingInventoryId = data[0].CruisePricingInventoryId
                });
            }

            return cabins;
        }
    }
}

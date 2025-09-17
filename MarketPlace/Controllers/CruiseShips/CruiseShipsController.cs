using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.PagedData;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Marketplace.API.Controllers.CruiseShips
{
    [ApiController]
    [Route("api/[controller]")]
    public class CruiseShipsController : ControllerBase
    {
        private readonly ICruiseShipService _cruiseShipService;
        private readonly ICruiseLineService _cruiseLineService;

        public CruiseShipsController(
            ICruiseShipService cruiseShipService,
            ICruiseLineService cruiseLineService)
        {
            _cruiseShipService = cruiseShipService;
            _cruiseLineService = cruiseLineService;
        }

        // GET: api/CruiseShips?page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PagedData<CruiseShipDto>>> GetShips(int page = 1, int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be greater than zero.");

            var allShips = await _cruiseShipService.GetAll() ?? new List<CruiseShipDto>();

            var totalCount = allShips.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pagedShips = allShips
                .OrderBy(s => s.ShipName) // stable ordering for pagination
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PagedData<CruiseShipDto>
            {
                Items = pagedShips,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            return Ok(result);
        }

        // POST: api/CruiseShips
        [HttpPost]
        public async Task<IActionResult> AddShip([FromBody] CruiseShipDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verify cruise line exists
            var cruiseLines = await _cruiseLineService.GetAll();
            var selectedCruiseLine = cruiseLines.FirstOrDefault(cl => cl.CruiseLineId == model.CruiseLineId);

            if (selectedCruiseLine == null)
                return BadRequest(new { Message = "Invalid Cruise Line selected." });

            // Ensure DTO is mapped correctly
            var cruiseShipDto = new CruiseShipDto
            {
                ShipName = model.ShipName,
                ShipCode = model.ShipCode,
                CruiseLineId = selectedCruiseLine.CruiseLineId ?? 0
            };

            var result = await _cruiseShipService.Insert(cruiseShipDto);

            if (result != null && result.CruiseShipId.HasValue)
                return CreatedAtAction(nameof(GetShips), new { page = 1, pageSize = 10 }, result);

            return StatusCode(500, new { Message = "Failed to add cruise ship." });
        }

        // DELETE: api/Ships/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteShip(int id)
        {
            var ship = await _cruiseShipService.GetById(id);
            if (ship == null)
            {
                return NotFound(new { status = 404, message = "Ship not found." });
            }

            await _cruiseShipService.Delete(id);
            return Ok(new { status = 200, message = "Ship deleted successfully." });
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateShips([FromBody] CruiseShipDto cruiseShipDto)
        {
            if (cruiseShipDto == null)
                return BadRequest("User data is required");

            var updatedUser = await _cruiseShipService.Update(cruiseShipDto);
            return Ok(updatedUser);
        }
        // GET: api/CruiseLines
        [HttpGet("CruiseLine")]
        public async Task<ActionResult<List<CruiseLineDto>>> GetAllCruiseLines()
        {
            var cruiseLines = await _cruiseLineService.GetAll();

            // Optional: sort by name
            var sortedLines = cruiseLines.OrderBy(c => c.CruiseLineName).ToList();

            return Ok(sortedLines);
        }
    }
}

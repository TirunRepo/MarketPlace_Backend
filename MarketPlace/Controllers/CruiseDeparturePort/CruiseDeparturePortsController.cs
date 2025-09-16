using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.PagedData;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers.CruiseDeparturePort
{
    [ApiController]
    [Route("api/[controller]")]
    public class CruiseDeparturePortsController : ControllerBase
    {
        private readonly IDeparturePortService _departurePortService;
        private readonly IDestinationService _destinationService;

        public CruiseDeparturePortsController(
            IDeparturePortService departurePortService,
            IDestinationService destinationService)
        {
            _departurePortService = departurePortService;
            _destinationService = destinationService;
        }

        // GET: api/DeparturePorts?page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PagedData<DeparturePortDto>>> GetDeparturePorts(int page = 1, int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be greater than zero.");

            var allDeparturePorts = await _departurePortService.GetAll() ?? new List<DeparturePortDto>();

            var totalCount = allDeparturePorts.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pagedDeparturePorts = allDeparturePorts
                .OrderBy(d => d.DeparturePortName) // stable ordering for pagination
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PagedData<DeparturePortDto>
            {
                Items = pagedDeparturePorts,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            return Ok(result);
        }

        // POST: api/DeparturePorts
        [HttpPost]
        public async Task<IActionResult> AddDeparturePort([FromBody] DeparturePortDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var destination = await _destinationService.GetByCode(model.DestinationCode);

            if (destination == null)
                return BadRequest(new { Message = "Invalid destination selected." });

            var departurePortDto = new DeparturePortDto
            {
                DeparturePortId = model.DeparturePortId,
                DeparturePortCode = model.DeparturePortCode,
                DeparturePortName = model.DeparturePortName,
                DestinationCode = destination.DestinationCode,
                CreatedAt = model.CreatedAt,
                CreatedBy = model.CreatedBy,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedOn = model.LastModifiedOn
            };

            var result = await _departurePortService.Insert(departurePortDto);

            if (result != null && result.DeparturePortId.HasValue)
                return CreatedAtAction(nameof(GetDeparturePorts), new { page = 1, pageSize = 10 }, result);

            return StatusCode(500, new { Message = "Failed to save departure port." });
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateDeparture([FromBody] DeparturePortDto departurePortDto)
        {
            if (departurePortDto == null)
                return BadRequest("User data is required");

            var updatedUser = await _departurePortService.Update(departurePortDto);
            return Ok(updatedUser);
        }
        // DELETE: api/DeparturePorts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeparture(int id)
        {
            var departure = await _departurePortService.GetById(id);
            if (departure == null)
            {
                return NotFound(new { message = "Departure port not found." });
            }

            await _departurePortService.Delete(id);
            return Ok(new { message = "Departure port deleted successfully." });
        }

        // GET: api/Destination
        [HttpGet("destination")]
        public async Task<ActionResult<IEnumerable<DestinationDto>>> GetAllDestinations()
        {
            try
            {
                var destinations = await _destinationService.GetAll();

                if (destinations == null || !destinations.Any())
                {
                    return NotFound(new { Message = "No destinations found." });
                }

                return Ok(destinations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching destinations.", Details = ex.Message });
            }
        }

    }
}

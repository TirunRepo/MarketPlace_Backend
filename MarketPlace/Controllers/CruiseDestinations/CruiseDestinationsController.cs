using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.PagedData;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers.CruiseDestinations
{
    [ApiController]
    [Route("api/[controller]")]
    public class CruiseDestinationsController : ControllerBase
    {
        private readonly IDestinationService _destinationService;

        public CruiseDestinationsController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        // GET: api/Destinations?page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PagedData<DestinationDto>>> GetDestinations(int page = 1, int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be greater than zero.");

            var allDestinations = await _destinationService.GetAll() ?? new List<DestinationDto>();

            var totalCount = allDestinations.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pagedDestinations = allDestinations
                .OrderBy(d => d.DestinationName) // stable ordering
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PagedData<DestinationDto>
            {
                Items = pagedDestinations,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            return Ok(result);
        }

        // POST: api/Destinations
        [HttpPost]
        public async Task<IActionResult> AddDestination([FromBody] DestinationDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dto = new DestinationDto
            {
                DestinationCode = model.DestinationCode,
                DestinationName = model.DestinationName
            };

            var result = await _destinationService.Insert(dto);

            if (result != null)
                return CreatedAtAction(nameof(GetDestinations), new { page = 1, pageSize = 10 }, result);

            return StatusCode(500, new { Message = "Failed to save destination." });
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateDestination([FromBody] DestinationDto destinationDto)
        {
            if (destinationDto == null)
                return BadRequest("User data is required");

            var updatedUser = await _destinationService.Update(destinationDto);
            return Ok(updatedUser);
        }

        // DELETE: api/Destinations/{code}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestination(string id)
        {
            var destination = await _destinationService.GetByCode(id);
            if (destination == null)
                return NotFound(new { message = "Destination not found." });

            await _destinationService.Delete(id);
            return Ok(new { message = "Destination deleted successfully." });
        }
    }
}

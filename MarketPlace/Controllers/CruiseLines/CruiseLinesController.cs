using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.PagedData;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers.CruiseLines
{
    [ApiController]
    [Route("api/[controller]")]
    public class CruiseLinesController : ControllerBase
    {
        private readonly ICruiseLineService _cruiseLineService;

        public CruiseLinesController(ICruiseLineService cruiseLineService)
        {
            _cruiseLineService = cruiseLineService;
        }

        // GET: api/CruiseLines?page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PagedData<CruiseLineDto>>> GetCruiseLines(int page = 1, int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be greater than zero.");

            var allCruiseLines = (await _cruiseLineService.GetAll()).AsQueryable();

            var totalCount = allCruiseLines.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pagedCruiseLines = allCruiseLines
                .OrderBy(c => c.CruiseLineName) // LINQ ordering for stable pagination
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PagedData<CruiseLineDto>
            {
                Items = pagedCruiseLines,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            return Ok(result);
        }

        // POST: api/CruiseLines
        [HttpPost]
        public async Task<IActionResult> AddCruiseLine([FromBody] CruiseLineDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cruiseLineService.Insert(model);

            if (result != null)
                return CreatedAtAction(nameof(GetCruiseLines), new { page = 1, pageSize = 10 }, result);

            return StatusCode(500, "Failed to add cruise line.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLine(int id)
        {
            try
            {
                var line = _cruiseLineService.GetById(id);
                if (line == null)
                {
                    return NotFound(new { Message = "Cruise line not found." });
                }

                _cruiseLineService.Delete(id);

                return Ok(new { Message = "Cruise line deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the line.", Details = ex.Message });
            }
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateLines([FromBody] CruiseLineDto cruiseLineDto)
        {
            if (cruiseLineDto == null)
                return BadRequest("User data is required");

            var updatedUser = await _cruiseLineService.Update(cruiseLineDto);
            return Ok(updatedUser);
        }
    }
}

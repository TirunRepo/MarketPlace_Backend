using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Common.APIResponse;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.DTOs.ResponseModels.Inventory;
using MarketPlace.Common.PagedData;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        public async Task<IActionResult> GetShips(int page = 1, int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new APIResponse<PagedData<CruiseShipReponse>>
                {
                    Success = false,
                    Data = null,
                    Message = "Page and pageSize must be greater than zero."
                });
            }

            var pagedShips = await _cruiseShipService.GetList(page, pageSize);

            return Ok(new APIResponse<PagedData<CruiseShipReponse>>
            {
                Success = true,
                Data = pagedShips,
                Message = "Cruise ships retrieved successfully."
            });
        }

        // POST: api/CruiseShips
        [HttpPost]
        public async Task<IActionResult> AddShip([FromBody] CruiseShipRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new APIResponse<CruiseLineRequest>
                {
                    Success = false,
                    Data = null,
                    Message = "Invalid model data."
                });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.RecordBase = new()
            {
                Id = Convert.ToInt32(userId),
                CreatedBy = User.Identity.Name.ToString(),
                CreatedOn = DateTime.Now,
            };
            var result = await _cruiseShipService.Insert(model);

            if (result != null)
            {
                var response = new APIResponse<CruiseShipRequest>
                {
                    Success = true,
                    Data = result,
                    Message = "Cruise line added successfully."
                };

                return Ok(response); // ✅ simple success response
            }

            return StatusCode(StatusCodes.Status500InternalServerError,
                new APIResponse<CruiseLineRequest>
                {
                    Success = false,
                    Data = null,
                    Message = "Failed to add cruise line."
                });
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateShips(int Id,[FromBody] CruiseShipRequest model)
        {
            if (model == null)
            {
                return BadRequest(new APIResponse<CruiseShipRequest>
                {
                    Success = false,
                    Data = null,
                    Message = "Cruise line data is required."
                });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new APIResponse<CruiseShipRequest>
                {
                    Success = false,
                    Data = null,
                    Message = "User not authorized."
                });
            }

            // ✅ set updated info
            model.RecordBase ??= new(); // ensure not null
            model.RecordBase.UpdatedBy = User.Identity?.Name;
            model.RecordBase.UpdatedOn = DateTime.Now;
            model.RecordBase.Id = Convert.ToInt32(userId);

            var updated = await _cruiseShipService.Update(Id, model);

            if (updated == null)
            {
                return NotFound(new APIResponse<CruiseShipRequest>
                {
                    Success = false,
                    Data = null,
                    Message = $"Cruise line with ID {Id} not found."
                });
            }

            return Ok(new APIResponse<CruiseShipRequest>
            {
                Success = true,
                Data = updated,
                Message = "Cruise line updated successfully."
            });
        }
        // DELETE: api/Ships/5
        [HttpDelete("{id:int}")]
        public async Task<bool> DeleteShip(int id)
        {
            try
            {
                var line = await _cruiseShipService.GetById(id);
                if (line == null)
                {
                    return false; // not found
                }

                return await _cruiseShipService.Delete(id); ; // deleted successfully
            }
            catch
            {
                return false; // error occurred
            }
           
        }
    }
}

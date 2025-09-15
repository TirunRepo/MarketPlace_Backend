using MarketPlace.Common.APIResponse;
using MarketPlace.Common.DTOs.RequestModels.Promotions;
using MarketPlace.Common.DTOs.ResponseModels.Promotions;
using MarketPlace.Infrastucture.Promotion.Commands.CalculatePromotion;
using MarketPlace.Infrastucture.Promotion.Commands.CreatePromotion;
using MarketPlace.Infrastucture.Promotion.Commands.UpdatePromotion;
using MarketPlace.Infrastucture.Promotion.Queries.GetPromotionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Promotion.Infrastructure.Queries;

namespace Marketplace.API.Controllers.Promotions
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PromotionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse<PromotionResponse>>> Create([FromBody] PromotionRequest request)
        {
            var result = await _mediator.Send(new CreatePromotionCommand(request));
            return Created("", APIResponse<PromotionResponse>.Ok(result, "Promotion created successfully."));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse<PromotionResponse>>> Get(int id)
        {
            var result = await _mediator.Send(new GetPromotionByIdQuery(id));
            if (result == null)
                return NotFound(APIResponse<PromotionResponse>.Fail("Promotion not found."));
            return Ok(APIResponse<PromotionResponse>.Ok(result));
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse<List<PromotionResponse>>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllPromotionsQuery());
            return Ok(APIResponse<List<PromotionResponse>>.Ok(result));
        }

        [HttpPut]
        public async Task<ActionResult<APIResponse<PromotionResponse>>> Update([FromBody] PromotionRequest request)
        {
            var result = await _mediator.Send(new UpdatePromotionCommand(request));
            return Ok(APIResponse<PromotionResponse>.Ok(result, "Promotion updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse<string>>> Delete(int id)
        {
            await _mediator.Send(new DeletePromotionCommand(id));
            return Ok(APIResponse<string>.Ok("Promotion deleted successfully."));
        }

        [HttpPost("calculate-discount")]
        public async Task<IActionResult> CalculateDiscount([FromBody] PromotionCalculationRequest request)
        {
            var result = await _mediator.Send(new CalculatePromotionCommand(request));
            return Ok(result);
        }
    }
}

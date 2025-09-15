using MarketPlace.Common.APIResponse;
using MarketPlace.Common.DTOs.RequestModels.Markup;
using MarketPlace.Common.DTOs.ResponseModels.Markup;
using MarketPlace.Infrastucture.Markup.Commands.CalculateMarkup;
using MarketPlace.Infrastucture.Markup.Commands.CreateMarkup;
using MarketPlace.Infrastucture.Markup.Commands.DeleteMarkup;
using MarketPlace.Infrastucture.Markup.Commands.UpdateMarkup;
using MarketPlace.Infrastucture.Markup.Queries.GetAllMarkups;
using MarketPlace.Infrastucture.Markup.Queries.GetMarkupById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers.Markup
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MarkupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse<MarkupResponse>>> Create([FromBody] MarkupRequest request)
        {
            var result = await _mediator.Send(new CreateMarkupCommand(request));
            return Created("", APIResponse<MarkupResponse>.Ok(result, "Markup created successfully."));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse<MarkupResponse>>> Get(int id)
        {
            var result = await _mediator.Send(new GetMarkupByIdQuery(id));
            if (result == null)
                return NotFound(APIResponse<MarkupResponse>.Fail("Markup not found."));
            return Ok(APIResponse<MarkupResponse>.Ok(result));
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse<List<MarkupResponse>>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllMarkupsQuery());
            return Ok(APIResponse<List<MarkupResponse>>.Ok(result));
        }

        [HttpPut]
        public async Task<ActionResult<APIResponse<MarkupResponse>>> Update([FromBody] MarkupRequest request)
        {
            var result = await _mediator.Send(new UpdateMarkupCommand(request));
            return Ok(APIResponse<MarkupResponse>.Ok(result, "Markup updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse<string>>> Delete(int id)
        {
            await _mediator.Send(new DeleteMarkupCommand(id));
            return Ok(APIResponse<string>.Ok("Markup deleted successfully."));
        }

        [HttpPost("calculate-markup")]
        public async Task<IActionResult> CalculateDiscount([FromBody] MarkupCalculationRequest request)
        {
            var result = await _mediator.Send(new CalculateMarkupCommand(request));
            return Ok(result);
        }
    }
}

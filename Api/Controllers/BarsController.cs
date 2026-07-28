using Application.DTOs.Bars;
using Application.Features.Bars.AddBar;
using Application.Features.Bars;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Bars.DeleteBar;
using Application.Features.Bars.GetBar;
using Application.Features.Bars.GetBarsList;
using Application.Features.Bars.UpdateBar;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BarsController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BarResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<BarResponse> AddBar([FromBody] AddBarCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{barId}/delete")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ChangeStateBarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ChangeStateBarResponse> DeleteBar(Guid barId,
            [FromBody] DeleteBarCommand command)
        {
            command.BarId = barId;
            return await _mediator.Send(command);
        }

        [HttpGet("{barId}/bars")]
        [ProducesResponseType(typeof(BarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<BarResponse> GetBar(Guid barId)
        {
            return await _mediator.Send(new GetBarQuery { BarId = barId});
        }

        [HttpGet("all-bars")]
        [ProducesResponseType(typeof(BarListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<BarListResponse> GetBarsList()
        {
            return await _mediator.Send(new GetBarsListQuery());
        }

        [HttpPut("{barId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<BarResponse> UpdateBar(Guid barId, 
            [FromBody]UpdateBarCommand command)
        {
            command.BarId = barId;
            return await _mediator.Send(command);
        }
    }
}

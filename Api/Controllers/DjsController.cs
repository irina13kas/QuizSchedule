using Application.DTOs.Djs;
using Application.Features.Djs.AddDj;
using Application.Features.Djs.DeleteDj;
using Application.Features.Djs.GetDj;
using Application.Features.Djs.GetDjsList;
using Application.Features.Djs.UpdateDj;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DjsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DjsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(DjResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<DjResponse> AddDj([FromBody] AddDjCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{djId}/delete")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ChangeStateDjResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ChangeStateDjResponse> DeleteDj(Guid djId,
            [FromBody] DeleteDjCommand command)
        {
            command.DjId = djId;
            return await _mediator.Send(command);
        }

        [HttpGet("{djId}/djs")]
        [ProducesResponseType(typeof(DjResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<DjResponse> GetDj(Guid djId)
        {
            return await _mediator.Send(new GetDjQuery { DjId = djId });
        }

        [HttpGet("all-djs")]
        [ProducesResponseType(typeof(DjsListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<DjsListResponse> GetDjsList()
        {
            return await _mediator.Send(new GetDjsListQuery());
        }

        [HttpPut("{djId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(DjResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<DjResponse> UpdateDj(Guid djId,
            [FromBody] UpdateDjCommand command)
        {
            command.DjId = djId;
            return await _mediator.Send(command);
        }
    }
}

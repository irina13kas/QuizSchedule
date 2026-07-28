
using Application.DTOs.Masters;
using Application.Features.Masters.AddMaster;
using Application.Features.Masters.DeleteMaster;
using Application.Features.Masters.GetMaster;
using Application.Features.Masters.GetMastersList;
using Application.Features.Masters.UpdateMaster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MastersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MastersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(MasterResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<MasterResponse> AddMaster([FromBody] AddMasterCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{masterId}/delete")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ChangeStateMasterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ChangeStateMasterResponse> DeleteMaster(Guid masterId,
            [FromBody] DeleteMasterCommand command)
        {
            command.MasterId = masterId;
            return await _mediator.Send(command);
        }

        [HttpGet("{masterId}/masters")]
        [ProducesResponseType(typeof(MasterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<MasterResponse> GetMaster(Guid masterId)
        {
            return await _mediator.Send(new GetMasterQuery { MasterId = masterId });
        }

        [HttpGet("all-masters")]
        [ProducesResponseType(typeof(MastersListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<MastersListResponse> GetMastersList()
        {
            return await _mediator.Send(new GetMastersListQuery());
        }

        [HttpPut("{masterId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(MasterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<MasterResponse> UpdateMaster(Guid masterId,
            [FromBody] UpdateMasterCommand command)
        {
            command.MasterId = masterId;
            return await _mediator.Send(command);
        }
    }
}

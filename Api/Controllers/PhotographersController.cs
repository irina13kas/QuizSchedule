using Application.DTOs.Photographers;
using Application.Features.Photographers.AddPhotographer;
using Application.Features.Photographers.DeletePhotographer;
using Application.Features.Photographers.GetPhotographer;
using Application.Features.Photographers.GetPhotographersList;
using Application.Features.Photographers.UpdatePhotographer;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotographersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhotographersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(PhotographerResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PhotographerResponse> AddPhotographer([FromBody] AddPhotographerCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{photographerId}/delete")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ChangeStatePhotographerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ChangeStatePhotographerResponse> DeletePhotographer(Guid photographerId,
            [FromBody] DeletePhotographerCommand command)
        {
            command.PhotographerId = photographerId;
            return await _mediator.Send(command);
        }

        [HttpGet("{photographerId}/photographers")]
        [ProducesResponseType(typeof(PhotographerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PhotographerResponse> GetPhotographer(Guid photographerId)
        {
            return await _mediator.Send(new GetPhotographerQuery { PhotographerId = photographerId });
        }

        [HttpGet("all-photographers")]
        [ProducesResponseType(typeof(PhotographersListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<PhotographersListResponse> GetPhotographersList()
        {
            return await _mediator.Send(new GetPhotographersListQuery());
        }

        [HttpPut("{photographerId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(PhotographerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PhotographerResponse> UpdatePhotographer(Guid photographerId,
            [FromBody] UpdatePhotographerCommand command)
        {
            command.PhotographerId = photographerId;
            return await _mediator.Send(command);
        }
    }
}

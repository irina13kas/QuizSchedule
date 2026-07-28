using Application.DTOs.Points;
using Application.DTOs.Smart;
using Application.Features.PointsHistory.DeletePoints;
using Application.Features.SmartSchedule.AddToSmart;
using Application.Features.SmartSchedule.DeleteFromSmart;
using Application.Features.SmartSchedule.GetAvailableQuizmen;
using Application.Features.SmartSchedule.GetQuizmanAvability;
using Application.Features.SmartSchedule.UpdateSmart;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SmartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Quizman")]
        [ProducesResponseType(typeof(SmartResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<SmartResponse> AddToSmart([FromBody] AddToSmartCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{smartId}")]
        [Authorize(Roles = "Quizman")]
        [ProducesResponseType(typeof(ChangeStateSmartResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ChangeStateSmartResponse> DeleteSmart(Guid smartId)
        {
            return await _mediator.Send(new DeleteFromSmartCommand { SmartId = smartId });
        }

        [HttpGet("all-available-quizmen")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(AvailableQuizmenForDateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<AvailableQuizmenForDateResponse> GetAvailableQuizmenForDate(
            [FromBody]GetAvailableQuizmenForDateCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("get-quizman-availability-for-days")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(AvailableDaysForQuizmanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<AvailableDaysForQuizmanResponse> GetQuizmanAvailability(
            [FromBody] GetQuizmanAvailabilityCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{smartId}")]
        [Authorize(Roles = "Quizman")]
        [ProducesResponseType(typeof(SmartResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<SmartResponse> UpdateSmart(Guid smartId, 
            [FromBody] UpdateSmartCommand command)
        {
            command.SmartId = smartId;
            return await _mediator.Send(command);
        }
    }
}

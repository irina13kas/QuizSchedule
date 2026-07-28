using Application.DTOs.Schedule;
using Application.Features.Schedule.GetGamesList;
using Application.Features.Schedule.GetWeekSchedule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("daily-schedule")]
        [ProducesResponseType(typeof(DailyScheduleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<DailyScheduleResponse> GetDailySchedule([FromBody] GetDailyScheduleCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("week-schedule")]
        [ProducesResponseType(typeof(ScheduleMatrixResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ScheduleMatrixResponse> GetWeekSchedule([FromBody] GetWeekScheduleCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}

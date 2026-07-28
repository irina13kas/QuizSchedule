using Application.DTOs.Points;
using Application.Features.PointsHistory.AddPoints;
using Application.Features.PointsHistory.DeletePoints;
using Application.Features.PointsHistory.GetMyPointsHistory;
using Application.Features.PointsHistory.GetQuizemanPointsHistory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointsHistoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PointsHistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("points-history")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(PointsResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PointsResponse> AddPoints([FromBody] AddPointsCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{pointsId}/delete")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(PointsChangeStateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PointsChangeStateResponse> DeletePoints(Guid pointsId)
        {
            return await _mediator.Send(new DeleteSmartCommand {PointsId = pointsId });
        }

        [HttpPut("discard")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(PointsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PointsResponse> DiscardPoints([FromBody] DiscardPointsCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("get-my-points-history")]
        [Authorize]
        [ProducesResponseType(typeof(MyPointsHistoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<MyPointsHistoryResponse> GetMyPointsHistory(
            [FromBody] GetMyPointsHistoryCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("{quizmanId}/get-quizman-points-history")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(QuimanPointsHistoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<QuimanPointsHistoryResponse> GetQuizmanPointsHistory(
            Guid quizmanId,
            [FromBody] GetQuizmanPointsHistoryCommand command)
        {
            command.QuizmanId = quizmanId;
            return await _mediator.Send(command);
        }
    }
}

using Application.DTOs.Fines;
using Application.Features.FinesHistory.AddFine;
using Application.Features.FinesHistory.CloseFine;
using Application.Features.FinesHistory.DeleteFine;
using Application.Features.FinesHistory.GetAllFines;
using Application.Features.FinesHistory.GetQuizemenFinesHistory;
using Application.Features.FinesHistory.UpdateFine;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinesHistoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinesHistoryController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(AddFineResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<AddFineResponse> AddFine([FromBody] AddFineCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}/close")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(FineChangeStateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<FineChangeStateResponse> CloseFine(Guid id)
        {
            return await _mediator.Send(new CloseFineCommand { FineId = id} );
        }

        [HttpDelete("{fineId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(FineChangeStateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<FineChangeStateResponse> DeleteFine(Guid fineId)
        {
            return await _mediator.Send(new DeleteFineCommand {Id = fineId });
        }

        [HttpGet("get-my-fines-history")]
        [Authorize(Roles = "Quizman")]
        [ProducesResponseType(typeof(MyFinesHistoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<MyFinesHistoryResponse> GetMyFineHistory(
            [FromBody] GetMyFineHistoryQuery command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("{quizmanId}/get-quizman-fines-history")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(QuizmanFinesHistoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<QuizmanFinesHistoryResponse> GetQuizmanFinesHistory(
            Guid quizmanId, GetQuizmanFinesHistoryQuery command)
        {
            command.QuizmanId = quizmanId;
            return await _mediator.Send(command);
        }

        [HttpPut("{fineId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(QuizmanFinesHistoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdateFineResponse> UpdateFine(
            Guid fineId, 
            [FromBody] UpdateFineCommand command)
        {
            command.Id = fineId;
            return await _mediator.Send(command);
        }
    }
}

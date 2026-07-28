using Application.DTOs.Replacements;
using Application.Features.Replacements.AddReplacement;
using Application.Features.Replacements.DeleteReplacement;
using Application.Features.Replacements.GetQuizmanReplacements;
using Application.Features.Replacements.GetReplacements;
using Application.Features.Replacements.TakeReplacement;
using Application.Features.Replacements.UpdateReplacement;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReplacementsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReplacementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ReplacementResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ReplacementResponse> AddReplacement([FromBody] AddReplacementCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{replacementId}")]
        [Authorize]
        [ProducesResponseType(typeof(ChangeStateReplacement), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ChangeStateReplacement> DeleteReplacement(Guid replacementId)
        {
            return await _mediator.Send(new DeleteReplacementCommand { ReplacementId = replacementId});
        }

        [HttpGet("my-replacements")]
        [Authorize(Roles = "Quizman")]
        [ProducesResponseType(typeof(GetReplacementsResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetReplacementsResponse> GetMyReplacements([FromBody]GetMyReplacementsCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("{quizmanId}/quizman-replacements")]
        [Authorize(Roles = "Quizman")]
        [ProducesResponseType(typeof(GetReplacementsResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetReplacementsResponse> GetMyReplacements(Guid quizmanId,
            [FromBody]GetQuizmanReplacementsCommand command)
        {
            command.QuizmanId = quizmanId;
            return await _mediator.Send(command);
        }

        [HttpPost("{replacementId}/take")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(TakeReplacementResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<TakeReplacementResponse> TakeReplacement(Guid replacementId)
        {
            return await _mediator.Send(new TakeReplacementCommand { ReplacementId = replacementId});
        }

        [HttpPut("{replacementId}")]
        [Authorize]
        [ProducesResponseType(typeof(UpdateReplacementResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdateReplacementResponse> TakeReplacement(Guid replacementId,
            [FromBody]UpdateReplacementCommand command)
        {
            command.ReplacementId = replacementId;
            return await _mediator.Send(command);
        }
    }
}

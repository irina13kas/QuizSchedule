using Application.DTOs.Games;
using Application.Features.Games.CancelGame;
using Application.Features.Games.ConfirmGameWork;
using Application.Features.Games.CreateGame;
using Application.Features.Games.DeleteGame;
using Application.Features.Games.GetGame;
using Application.Features.Games.PublishGame;
using Application.Features.Games.UpdateGame;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GameResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GameResponse>> CreateGame([FromBody] CreateGameCommand command)
        {
            var game = await _mediator.Send(command);
            return CreatedAtAction(nameof(Application.Features.Games.GetGame), new { id = game.Id}, game);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GameWithParticipantsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GameWithParticipantsResponse> GetGame(Guid id)
        {
            return await _mediator.Send(new GetGameQuery{ GameId = id });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<GameResponse> UpdateGame(Guid id, [FromBody] UpdateGameCommand command)
        {
            command.GameId = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GameChangeStateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<GameChangeStateResponse> DeleteGame(Guid id)
        {
            return await _mediator.Send(new DeleteGameCommand { GameId = id });
        }

        [HttpPost("{id}/publish")]
        [ProducesResponseType(typeof(GameChangeStateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<GameChangeStateResponse> PublishGame(Guid id)
        {
            return await _mediator.Send(new PublishGameCommand { GameId = id});
        }

        [HttpPost("{id}/cancel")]
        [ProducesResponseType(typeof(GameChangeStateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<GameChangeStateResponse> CancelGame(Guid id, [FromBody] CancelGameCommand command)
        {
            command.GameId = id;
            return await _mediator.Send(command);
        }

        [HttpPost("{id}/confirmWorked")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> ConfirmGameWork(Guid id, [FromBody] ConfirmGameWorkCommand command)
        {
            command.GameId = id;
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

using Application.DTOs.Games;
using Application.Features.Participants.AddParticipantToGame;
using Application.Features.Participants.ChangeParticipantActivity;
using Application.Features.Participants.ChangeParticipantRole;
using Application.Features.Participants.ChangeShiftType;
using Application.Features.Participants.DeleteParticipantsFromGame;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/games/participants")]
    [Authorize(Roles = "Admin")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IMediator _midiator;

        public ParticipantsController(IMediator mediator)
        {
            _midiator = mediator;
        }

        [HttpPost("{gameId}/participant")]
        [ProducesResponseType(typeof(GameResponseForParticipants), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<GameResponseForParticipants> AddParticipantToGame( 
            Guid gameId,
            [FromBody] AddParticipantToGameCommand command)
        {
            command.GameId = gameId;
            return await _midiator.Send(command);
        }

        [HttpPut("{participantId}/activity")]
        [ProducesResponseType(typeof(GameResponseForParticipants), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<GameResponseForParticipants> ChangeParticipantActivity(
            Guid participantId,
            [FromBody]ChangeParticipantActivityCommand command)
        {
            command.ParticipantId = participantId;
            return await _midiator.Send(command);
        }

        [HttpPut("{participantId}/game-role")]
        [ProducesResponseType(typeof(GameResponseForParticipants), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<GameResponseForParticipants> ChangeParticipantRole(
            Guid participantId,
            [FromBody] ChangeParticipantRoleCommand command)
        {
            command.ParticipantId = participantId;
            return await _midiator.Send(command);
        }

        [HttpPut("{participantId}/shift-type")]
        [ProducesResponseType(typeof(GameResponseForParticipants), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<GameResponseForParticipants> ChangeShiftType(
            Guid participantId,
            [FromBody] ChangeShiftTypeCommand command)
        {
            command.ParticipantId = participantId;
            return await _midiator.Send(command);
        }

        [HttpDelete("{participantId}/participant")]
        [ProducesResponseType(typeof(GameResponseForParticipants), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<GameResponseForParticipants> DeleteParticipantFromGame(Guid participantId)
        {
            return await _midiator.Send(new DeleteParticipantFromGameCommand {ParticipantId = participantId });
        }
    }
}

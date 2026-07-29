using Application.DTOs.Games;
using Application.DTOs.Participants;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Participants.AddParticipantToGame
{
    public class AddParticipantToGameCommand : IRequest<GameWithParticipantsResponse>
    {
        public Guid QuizmanId { get; set; }
        public Guid GameId { get; set; }
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsFullShift { get; set; }
    }
}

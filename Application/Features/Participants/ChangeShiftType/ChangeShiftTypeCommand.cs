using Application.DTOs.Games;
using Application.DTOs.Participants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Participants.ChangeShiftType
{
    public class ChangeShiftTypeCommand : IRequest<GameWithParticipantsResponse>
    {
        public Guid ParticipantId { get; set; }
        public bool IsFullShift { get; set; }
    }
}

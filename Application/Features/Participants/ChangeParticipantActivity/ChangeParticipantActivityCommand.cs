using Application.DTOs.Games;
using Application.DTOs.Participants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Participants.ChangeParticipantActivity
{
    public class ChangeParticipantActivityCommand : IRequest<GameResponseForParticipants>
    {
        public Guid ParticipantId { get; set; }
        public bool IsActive { get; set; }
    }
}

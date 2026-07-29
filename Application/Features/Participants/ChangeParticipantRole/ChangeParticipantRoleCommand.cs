using Application.DTOs.Games;
using Application.DTOs.Participants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Participants.ChangeParticipantRole
{
    public class ChangeParticipantRoleCommand : IRequest<GameWithParticipantsResponse>
    {
        public Guid ParticipantId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}

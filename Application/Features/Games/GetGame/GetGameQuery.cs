using Application.DTOs.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.GetGame
{
    public class GetGameQuery : IRequest<GameWithParticipantsResponse>
    {
        public Guid GameId { get; set; }
    }
}

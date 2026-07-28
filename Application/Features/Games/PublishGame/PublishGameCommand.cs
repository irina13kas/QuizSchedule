using Application.DTOs.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.PublishGame
{
    public class PublishGameCommand : IRequest<GameChangeStateResponse>
    {
        public Guid GameId {get; set;}
    }
}

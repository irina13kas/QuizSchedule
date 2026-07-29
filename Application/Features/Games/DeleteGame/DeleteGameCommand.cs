using Application.DTOs.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.DeleteGame
{
    public class DeleteGameCommand : IRequest<GameChangeStateResponse>
    {
        public Guid GameId { get; set; }
    }
}

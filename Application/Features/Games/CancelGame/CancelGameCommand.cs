using Application.DTOs.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.CancelGame
{
    public class CancelGameCommand : IRequest<GameChangeStateResponse>
    {
        public Guid GameId { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}

using Application.DTOs.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.CreateGame
{
    public class CreateGameCommand : IRequest<GameResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Guid ResponsibleAdminId { get; set; }
        public Guid BarId { get; set; }
        public string? Partner { get; set; }
        public DateTime GameStartTime { get; set; }
        public DateTime WorkStartTime { get; set; }
        public Guid MasterId { get; set; }
        public Guid DjId { get; private set; }
        public Guid PhotographerId { get; private set; }
    }
}

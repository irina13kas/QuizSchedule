using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.ConfirmGameWork
{
    public class ConfirmGameWorkCommand : IRequest
    {
        public Guid GameId { get; set; }
        public List<Guid>? ExcludeQuizmanIds { get; set; }
    }
}

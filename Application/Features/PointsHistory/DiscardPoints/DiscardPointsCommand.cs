using Application.DTOs.Points;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PointsHistory.DeletePoints
{
    public class DiscardPointsCommand : IRequest<PointsResponse>
    {
        public Guid QuizmanId { get; set; }
        public Guid AdminId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Amount { get; set; }
    }
}

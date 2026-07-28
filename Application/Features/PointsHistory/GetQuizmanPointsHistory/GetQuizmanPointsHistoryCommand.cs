using Application.DTOs.Points;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PointsHistory.GetQuizemanPointsHistory
{
    public class GetQuizmanPointsHistoryCommand : IRequest<QuimanPointsHistoryResponse>
    {
        public Guid QuizmanId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Points
{
    public class PointsResponse
    {
        public Guid Id { get; set; }
        public Guid QuizmanId { get; set; }
        public string QuizmanName { get; set; } = string.Empty;
        public string AdminName { get; set; } = string.Empty;
        public Guid? GameId { get; set; }
        public string? GameName { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; } = string.Empty;
        public decimal PointsBalance { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}

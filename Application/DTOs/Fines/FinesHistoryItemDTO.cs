using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Fines
{
    public class FinesHistoryItemDTO
    {
        public Guid Id { get; set; }
        public Guid QuizmanId { get; set; }
        public string QuizmanName { get; set; } = string.Empty;
        public Guid AdminId { get; set; }
        public string AdminName { get; set; } = string.Empty;
        public int Amount { get; set; }
        public string Comment { get; set; } = string.Empty;
        public Guid GameId { get; set; }
        public string GameName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsClosed { get; set; }
    }
}

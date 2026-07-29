using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Smart
{
    public class SmartResponse
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public Guid QuizmanId { get; set; }
        public string QuizmanName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
}

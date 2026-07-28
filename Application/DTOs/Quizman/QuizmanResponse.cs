using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Quizman
{
    public class QuizmanResponse
    {
        public Guid QuizemanId { get; set; }
        public string QuizemanName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? Comment { get; set; }

    }
}

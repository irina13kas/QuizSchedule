using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Smart
{
    public class SmartDaysForQuizmanResponse
    {
        public Guid QuizmanId { get; set; }
        public string QuizemanName { get; set; } = string.Empty;
        public List<SmartItemResponse> SmartDaysForQuizman { get; set; } = new();
    }
}

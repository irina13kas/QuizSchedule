using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Smart
{
    public class AvailableDaysForQuizmanResponse
    {
        public Guid QuizmanId { get; set; }
        public string QuizemanName { get; set; } = string.Empty;
        public List<SmartItemResponse> AvailableDaysForQuizman { get; set; }
    }
}

using Application.DTOs.Quizman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Smart
{
    public class AvailableQuizmenForDateResponse
    {
        public DateOnly Date { get; set; }
        public List<QuizmanResponse> AvailableQuizmen { get; set; } = new();
    }
}

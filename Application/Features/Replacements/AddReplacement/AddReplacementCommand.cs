using Application.DTOs.Replacements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.AddReplacement
{
    public class AddReplacementCommand : IRequest<ReplacementResponse>
    {
        public Guid QuizmanId { get; set; }
        public string? Comment { get; set; }
        public bool IsFullShift { get; set; }
        public DateTime Date { get; set; }

    }
}

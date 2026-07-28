using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Replacements
{
    public class TakeReplacementResponse
    {
        public Guid ReplacementId { get; set; }
        public bool IsTaken { get; set; }
        public Guid QuizmanId { get; set; }
        public bool IsFullShift { get; set; }
        public string? Comment { get; set; }
    }
}

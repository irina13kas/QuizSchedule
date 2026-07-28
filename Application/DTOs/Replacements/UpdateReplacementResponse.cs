using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Replacements
{
    public class UpdateReplacementResponse
    {
        public Guid ReplacementId { get; set; }
        public string? Comment { get; set; }
        public bool IsFullShift { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}

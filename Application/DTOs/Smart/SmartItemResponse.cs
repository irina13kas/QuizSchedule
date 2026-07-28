using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Smart
{
    public class SmartItemResponse
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
}

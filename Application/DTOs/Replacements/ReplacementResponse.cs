using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Replacements
{
    public class ReplacementResponse
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public Guid QuizemanId { get; set; }
        public string QuizemanName { get; set; } = string.Empty;
        public string? Comment { get; set; }
        public bool IsFullShift { get; set; }
        public string Status { get; set; } = string.Empty ;
        public Guid? TakenAdminId { get; set; }
        public string? TakenAdminName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

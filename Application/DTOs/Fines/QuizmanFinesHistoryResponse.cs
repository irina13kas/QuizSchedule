using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Fines
{
    public class QuizmanFinesHistoryResponse
    {
        public Guid QuizmanId { get; set; }
        public string QuizmanName { get; set; } = string.Empty;
        public List<FinesHistoryItemDTO> Fines { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}

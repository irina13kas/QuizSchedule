using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Schedule
{
    public class ScheduleMatrixResponse
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<DailyScheduleResponse> ScheduleForWeek { get; set; } = new();
    }
}

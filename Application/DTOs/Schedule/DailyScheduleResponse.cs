using Application.DTOs.Games;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Schedule
{
    public class DailyScheduleResponse
    {
        public DateTime Date { get; set; }
        public string DayOfWeek { get; set; }
        public List<GameWithParticipantsResponse> Games { get; set; }

    }
}

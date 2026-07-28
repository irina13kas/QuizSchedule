using Application.DTOs.Schedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedule.GetGamesList
{
    public class GetDailyScheduleCommand : IRequest<DailyScheduleResponse>
    {
        public DateTime Date { get; set; }
    }
}

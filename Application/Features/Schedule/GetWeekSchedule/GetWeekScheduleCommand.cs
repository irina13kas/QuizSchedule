using Application.DTOs.Schedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedule.GetWeekSchedule
{
    public class GetWeekScheduleCommand : IRequest<ScheduleMatrixResponse>
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

    }
}

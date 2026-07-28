using Application.DTOs.Smart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.GetAvailableQuizmen
{
    public class GetAvailableQuizmenForDateCommand : IRequest<AvailableQuizmenForDateResponse>
    {
        public DateTime Date { get; set; }
    }
}

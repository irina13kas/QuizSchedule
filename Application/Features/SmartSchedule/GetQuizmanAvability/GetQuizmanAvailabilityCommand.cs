using Application.DTOs.Smart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.GetQuizmanAvability
{
    public class GetQuizmanAvailabilityCommand : IRequest<AvailableDaysForQuizmanResponse>
    {
        public Guid QuizmanId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}

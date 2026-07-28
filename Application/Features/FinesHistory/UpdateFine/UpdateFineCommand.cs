using Application.DTOs.Fines;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinesHistory.UpdateFine
{
    public class UpdateFineCommand : IRequest<UpdateFineResponse>
    {
        public Guid Id { get; set; }
        public Guid QuizmanId { get; set; }
        public Guid AdminId { get; set; }
        public int Amount { get; set; }
        public string Comment { get; set; } = string.Empty;
        public Guid? GameId { get; set; }
    }
}

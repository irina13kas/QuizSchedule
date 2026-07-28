using Application.DTOs.Profiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.DeleteQuizemanProfile
{
    public class DeleteQuizmanProfileCommand : IRequest<DeleteQuizmanProfileResponse>
    {
        public Guid QuizmanId { get; set; }
    }
}

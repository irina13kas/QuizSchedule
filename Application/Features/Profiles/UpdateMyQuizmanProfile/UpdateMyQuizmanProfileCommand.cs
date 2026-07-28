using Application.DTOs.Profiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.UpdateMyProfile
{
    public class UpdateMyQuizmanProfileCommand : IRequest<QuizmanProfileResponse>
    {
        public string? Name { get; set; }
        public string? Photo { get; set; }
        public string? VkUrl { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}

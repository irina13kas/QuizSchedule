using Application.DTOs.Quizman;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.GetQuizmenList
{
    public class GetQuizmenListQuery : IRequest<QuizmenListResponse>
    {
        public bool? IsActive { get; set; }
    }
}

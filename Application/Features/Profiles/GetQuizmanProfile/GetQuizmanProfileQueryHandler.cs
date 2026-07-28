using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Profiles;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.GetMyProfile
{
    public class GetQuizmanProfileQueryHandler : IRequestHandler<GetQuizmanProfileQuery, 
        QuizmanProfileResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IQuizmanRepository _quizmanRepository;

        public GetQuizmanProfileQueryHandler(ICurrentUserService currentUserService, 
            IQuizmanRepository quizmanRepository)
        {
            _currentUserService = currentUserService;
            _quizmanRepository = quizmanRepository;
        }

        public async Task<QuizmanProfileResponse> Handle(GetQuizmanProfileQuery request, 
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var quizman = await _quizmanRepository.GetByIdAsync(request.QuizmanId, cancellationToken);
            if (quizman == null)
                throw new NotFoundException("Квизмена не существует");

            if (_currentUserService.UserId == quizman.UserId || _currentUserService.Role == UserRole.Admin.ToString()) 
                return new QuizmanProfileResponse
                {
                    Id = quizman.Id,
                    Name = quizman.User.Name,
                    VkUrl = quizman.User.VkUrl,
                    PhotoUrl = quizman.User.PhotoUrl,
                    WorkShift = quizman.WorkShift,
                    Fines = quizman.Fines,
                    Points = quizman.Points
                };
            else
                return new QuizmanProfileResponse
                {
                    Id = quizman.Id,
                    Name = quizman.User.Name,
                    VkUrl = quizman.User.VkUrl,
                    PhotoUrl = quizman.User.PhotoUrl,
                    WorkShift = quizman.WorkShift,
                    Fines = null,
                    Points = null
                };
        }
    }
}

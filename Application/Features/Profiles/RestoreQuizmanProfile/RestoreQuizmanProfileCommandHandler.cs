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

namespace Application.Features.Profiles.RestoreQuizemanProfile
{
    public class RestoreQuizmanProfileCommandHandler : IRequestHandler<RestoreQuizmanProfileCommand,
        QuizmanProfileResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestoreQuizmanProfileCommandHandler(ICurrentUserService currentUserService, 
            IQuizmanRepository quizmanRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _quizmanRepository = quizmanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<QuizmanProfileResponse> Handle(RestoreQuizmanProfileCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может восстанавливать Квизмена");

            var quizman = await _quizmanRepository.GetByIdAsync(request.QuizmanId, cancellationToken);
            if (quizman == null)
                throw new NotFoundException("Квизмен не существует");

            _quizmanRepository.Restore(quizman);
            quizman.User.RestoreProfile();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new QuizmanProfileResponse
            {
                Id = quizman.Id,
                Name = quizman.User.Name,
                VkUrl = quizman.User.VkUrl,
                PhotoUrl = quizman.User.PhotoUrl,
                WorkShift = quizman.WorkShift,
                Points = quizman.Points,
                Fines = quizman.Fines,
            };
        }
    }
}

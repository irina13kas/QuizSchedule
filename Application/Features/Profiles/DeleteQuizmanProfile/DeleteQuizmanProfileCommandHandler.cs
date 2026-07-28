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

namespace Application.Features.Profiles.DeleteQuizemanProfile
{
    public class DeleteQuizmanProfileCommandHandler : IRequestHandler<DeleteQuizmanProfileCommand,
        DeleteQuizmanProfileResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IQuizmanRepository _quizemanRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteQuizmanProfileCommandHandler(ICurrentUserService currentUserService,
            IQuizmanRepository quizemanRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _quizemanRepository = quizemanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteQuizmanProfileResponse> Handle(DeleteQuizmanProfileCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Для удаления профиля Квизмена нужно обладать правами Админа");

            var quizeman = await _quizemanRepository.GetByIdAsync(request.QuizmanId);

            if (quizeman == null)
                throw new NotFoundException("Квизмена с таким Id не существует");

            _quizemanRepository.Remove(quizeman);
            quizeman.User.DeleteProfile();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DeleteQuizmanProfileResponse
            {
                IsDeleted = true,
            };
        }
    }
}

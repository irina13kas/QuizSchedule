using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Profiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.UpdateMyProfile
{
    public class UpdateMyQuizmanProfileCommandHandler : IRequestHandler<UpdateMyQuizmanProfileCommand,
        QuizmanProfileResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMyQuizmanProfileCommandHandler(ICurrentUserService currentUserService,
            IQuizmanRepository quizmanRepository, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _quizmanRepository = quizmanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<QuizmanProfileResponse> Handle(UpdateMyQuizmanProfileCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? 
                throw new UnauthorizedException();

            var quizman = await _quizmanRepository.GetByUserIdAsync(userId, cancellationToken);
            if (quizman == null)
                throw new NotFoundException("Квизмен не существует");

            if (quizman.User.VkUrl.Equals(request.VkUrl))
                throw new ExistInDBException("Квизмен с таким Вк уже существует");

            if (quizman.User.Id != _currentUserService.UserId)
                throw new ForbiddenException("Нельзя редактировать не свой профиль");

            quizman.User.UpdateContactInfo(request.Name, request.VkUrl, request.BirthDay,
                request.Photo);

            _quizmanRepository.Update(quizman);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new QuizmanProfileResponse
            {
                Id = quizman.Id,
                Name = quizman.User.Name,
                VkUrl = quizman.User.VkUrl,
                PhotoUrl = quizman.User.PhotoUrl,
                WorkShift = quizman.WorkShift,
                Fines = quizman.Fines,
                Points = quizman.Points,
            };
        }
    }
}

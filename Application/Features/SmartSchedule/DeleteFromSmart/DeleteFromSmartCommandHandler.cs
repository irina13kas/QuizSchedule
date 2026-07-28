using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Smart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.DeleteFromSmart
{
    public class DeleteFromSmartCommandHandler : IRequestHandler<DeleteFromSmartCommand,
        ChangeStateSmartResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISmartRepository _scheduleRepository;
        private readonly IQuizmanRepository _quizmanRepository;

        public DeleteFromSmartCommandHandler(
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork, 
            ISmartRepository scheduleRepository,
            IQuizmanRepository quizmanRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _scheduleRepository = scheduleRepository;
            _quizmanRepository = quizmanRepository;
        }

        public async Task<ChangeStateSmartResponse> Handle(DeleteFromSmartCommand request,
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var userId = _currentUserService.UserId ?? Guid.Empty;

            var quizman = await _quizmanRepository.GetByUserIdAsync(userId, cancellationToken);
            if (quizman == null)
                throw new NotFoundException("Квизмен не существует");

            if (quizman.User.Id != _currentUserService.UserId)
                throw new NotBusinessSuitableException("Нельзя менять Smart другого квизмена");

            var smartDay = await _scheduleRepository.GetByIdAsync(request.SmartId, cancellationToken);

            _scheduleRepository.Remove(smartDay);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ChangeStateSmartResponse
            {
                Successed = true,
            };
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Smart;
using Domain.Enums;
using MediatR;

namespace Application.Features.SmartSchedule.UpdateSmart
{
    public class UpdateSmartCommandHandler : IRequestHandler<UpdateSmartCommand,
        SmartResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ISmartRepository _scheduleRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSmartCommandHandler(ICurrentUserService currentUserService,
            ISmartRepository scheduleRepository,
            IQuizmanRepository quizmanRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _scheduleRepository = scheduleRepository;
            _quizmanRepository = quizmanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SmartResponse> Handle(UpdateSmartCommand request, 
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var userId = _currentUserService.UserId ?? Guid.Empty;

            var quizman = await _quizmanRepository.GetByUserIdAsync(userId, cancellationToken);

            Enum.TryParse<SmartStatus>(request.Status, true, out SmartStatus status);
            
            var quizmanDaySmart = await _scheduleRepository.GetByIdAsync(request.SmartId, 
                cancellationToken);

            if (quizmanDaySmart == null)
                throw new NotFoundException("Smart квизмена не существует");

            quizmanDaySmart.Update(status, request.Comment);
            _scheduleRepository.Update(quizmanDaySmart);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new SmartResponse
            {
                Id = quizmanDaySmart.Id,
                Comment = quizmanDaySmart.Comment,
                Date = quizmanDaySmart.Date,
                Status = quizmanDaySmart.Status.ToString(),
                QuizmanId = quizman.Id,
                QuizmanName = quizman.User.Name
            };

        }
    }
}

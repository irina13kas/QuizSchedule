using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Smart;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.SmartSchedule.AddToSmart
{
    public class AddToSmartCommandHandler : IRequestHandler<AddToSmartCommand,
        SmartResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ISmartRepository _scheduleRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddToSmartCommandHandler(ICurrentUserService currentUserService, 
            ISmartRepository scheduleRepository,
            IQuizmanRepository quizmanRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _scheduleRepository = scheduleRepository;
            _quizmanRepository = quizmanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SmartResponse> Handle(AddToSmartCommand request, 
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var userId = _currentUserService.UserId ?? Guid.Empty;

            var quizman = await _quizmanRepository.GetByUserIdAsync(userId, cancellationToken);

            var existSmart = await _scheduleRepository.GetByIdAndDateAsync(
                quizman.Id,
                request.Date,
                cancellationToken);
            if (existSmart != null)
                throw new ExistInDBException("Смарт на этот день уже заполнен пользователем");

            Enum.TryParse<SmartStatus>(request.Status, true, out SmartStatus status);
            var quizmanDaySmart = new Smart(DateOnly.FromDateTime(request.Date), 
                status, 
                quizman.Id,
                request.Comment ?? "");

            await _scheduleRepository.AddAsync(quizmanDaySmart, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new SmartResponse
            {
                Id = quizmanDaySmart.Id,
                Comment = quizmanDaySmart.Comment,
                Date = quizmanDaySmart.Date,
                Status = quizmanDaySmart.Status.ToString(),
                QuizemanId = quizman.Id,
                QuizemanName = quizman.User.Name
            };
        }
    }
}

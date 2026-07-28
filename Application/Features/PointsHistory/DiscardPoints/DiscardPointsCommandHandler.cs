using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Points;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.PointsHistory.DeletePoints
{
    public class DiscardPointsCommandHandler : IRequestHandler<DiscardPointsCommand, PointsResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IGameRepository _gameRepository;
        private readonly IPointsRepository _pointsRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DiscardPointsCommandHandler(ICurrentUserService currentUserService, IGameRepository gameRepository, IPointsRepository pointsRepository, IAdminRepository adminRepository, IQuizmanRepository quizmanRepository, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _gameRepository = gameRepository;
            _pointsRepository = pointsRepository;
            _adminRepository = adminRepository;
            _quizmanRepository = quizmanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PointsResponse> Handle(DiscardPointsCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может списывать баллы");

            var quizman = await _quizmanRepository.GetByIdAsync(request.QuizmanId, cancellationToken);
            if (quizman == null)
                throw new NotFoundException("Квизмена не существует");

            var admin = await _adminRepository.GetByIdAsync(request.AdminId, cancellationToken);
            if (admin == null)
                throw new NotFoundException("Админ не существует");

            var points = new Point(request.QuizmanId, request.AdminId, -request.Amount,
                request.Comment);

            quizman.RemovePoints(points);

            await _pointsRepository.AddAsync(points, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new PointsResponse
            {
                Id = points.Id,
                QuizmanId = quizman.Id,
                QuizmanName = quizman.User.Name,
                AdminName = admin.User.Name,
                Amount = -request.Amount,
                Comment = request.Comment,
                PointsBalance = quizman.Points,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}

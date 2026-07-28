using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Points;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;

namespace Application.Features.PointsHistory.AddPoints
{
    public class AddPointsCommandHandler : IRequestHandler<AddPointsCommand, PointsResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IPointsRepository _pointsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddPointsCommandHandler(ICurrentUserService currentUserService, IQuizmanRepository quizmanRepository, 
            IAdminRepository adminRepository, IUnitOfWork unitOfWork,
            IGameRepository gameRepository,
            IPointsRepository pointsRepository)
        {
            _currentUserService = currentUserService;
            _quizmanRepository = quizmanRepository;
            _adminRepository = adminRepository;
            _pointsRepository = pointsRepository;
            _gameRepository = gameRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PointsResponse> Handle(AddPointsCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может начислять баллы");

            var quizman = await _quizmanRepository.GetByIdAsync(request.QuizmanId, cancellationToken);
            if (quizman == null)
                throw new NotFoundException("Квизмена не существует");

            var admin = await _adminRepository.GetByIdAsync(request.AdminId, cancellationToken);
            if (admin == null)
                throw new NotFoundException("Админа не существует");

            var game = await _gameRepository.GetByIdAsync(request.GameId, cancellationToken);
            if (game == null)
                throw new NotFoundException("Игры не существует");

            var points = new Point(request.QuizmanId, request.AdminId,
                request.Amount, request.Comment, request.GameId);

            quizman.AddPoints(points);

            await _pointsRepository.AddAsync(points);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new PointsResponse
            {
                Id = points.Id,
                QuizmanId = request.QuizmanId,
                QuizmanName = quizman.User.Name,
                AdminName = admin.User.Name,
                GameId = request.GameId,
                GameName = game.Name,
                Amount = points.Amount,
                Comment = points.Comment,
                PointsBalance = quizman.Points,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}

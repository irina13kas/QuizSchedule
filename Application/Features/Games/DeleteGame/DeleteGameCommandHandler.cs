using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Games;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.DeleteGame
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, GameChangeStateResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IGameRepository _gameRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGameCommandHandler(ICurrentUserService currentUserService,
            IGameRepository gameRepository,
            INotificationRepository notificationRepository,   
            IAdminRepository adminRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _gameRepository = gameRepository;
            _notificationRepository = notificationRepository;
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GameChangeStateResponse> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Для удаления Игры нужно обладать правами Администратора");

            var game = await _gameRepository.GetByIdAsync(request.GameId, cancellationToken);
            if (game == null)
                throw new NotFoundException("Игры с таким Id не существует");

            var userId = _currentUserService.UserId ?? throw new UnauthorizedException();

            var admin = await _adminRepository.GetByUserIdAsync(userId, cancellationToken);
            if (admin == null)
                throw new NotFoundException("Админ не существует");

            _gameRepository.Remove(game);
            await NotifyParticipantsAsync(admin.Id, game, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new GameChangeStateResponse
            {
                Success = true
            };
        }

        private async Task NotifyParticipantsAsync(
            Guid adminId,
            Game game,
            CancellationToken cancellationToken)
        {
            if (game.Participants == null || !game.Participants.Any())
                return;

            string title = $"Игра \"{game.Name}\" удалена";
            string message = $"Игра в баре {game.Bar?.Name ?? "не указан"} " +
                      $"на {game.GameStartTime:dd.MM.yyyy HH:mm} удалена. ";

            foreach (var p in game.Participants)
            {
                var notification = new Notification(p.Id, adminId, message, title, NotificationType.GameCancelled);
                await _notificationRepository.AddAsync(notification, cancellationToken);
            }
        }
    }
}

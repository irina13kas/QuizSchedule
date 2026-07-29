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

namespace Application.Features.Games.CancelGame
{
    public class CancelGameCommandHandler : IRequestHandler<CancelGameCommand, GameChangeStateResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameRepository _gameRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IGameParticipantRepository _participantRepository;
        private readonly IAdminRepository _adminRepository;

        public CancelGameCommandHandler(
            IAdminRepository adminRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork, IGameRepository gameRepository,
            INotificationRepository notificationRepository, 
            IGameParticipantRepository participantRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _gameRepository = gameRepository;
            _notificationRepository = notificationRepository;
            _participantRepository = participantRepository;
            _adminRepository = adminRepository;
        }

        public async Task<GameChangeStateResponse> Handle(CancelGameCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Отменять Игру может только Администратор");

            var game = await _gameRepository.GetByIdWithParticipantsAsync(request.GameId, cancellationToken);

            if (game == null)
                throw new NotFoundException("Игра не найдена");
            var userId = _currentUserService.UserId ?? throw new UnauthorizedException();

            var admin = await _adminRepository.GetByUserIdAsync(userId, cancellationToken);
            if (admin == null)
                throw new NotFoundException("Админа не существует");

            if (game.Status == GameStatus.Cancelled)
                throw new NotBusinessSuitableException("Нельзя отменить уже отмененную игру");

            if (game.GameStartTime < DateTime.Today)
                throw new NotBusinessSuitableException("Нельзя отменить Игру, которая уже прошла");

            var allParticipants = game.Participants;

            foreach ( var participant in allParticipants)
            {
                game.RemoveQuizeman(participant.QuizmanId);
            }

            //_participantRepository.DeleteAllParticipantsFromGame(game.Id, cancellationToken);

            game.Cancel();
            await NotifyParticipantsAsync(admin.Id, game, request.Reason, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new GameChangeStateResponse
            {
                Success = true
            };
        }

        private async Task NotifyParticipantsAsync(
            Guid adminId,
            Game game,
            string reason,
            CancellationToken cancellationToken)
        {
            if (game.Participants == null || !game.Participants.Any())
                return;

            string title = $"Игра \"{game.Name}\" отменена";
            string message = $"Игра в баре {game.Bar?.Name ?? "не указан"} " +
                      $"на {game.GameStartTime:dd.MM.yyyy HH:mm} отменена. " +
                      $"Причина: {reason}";

            foreach(var p in game.Participants){
                var notification = new Notification(p.Id, adminId, message,title, NotificationType.GameCancelled);
                await _notificationRepository.AddAsync(notification, cancellationToken);
            }
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Games;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.PublishGame
{
    public class PublishGameCommandHandler : IRequestHandler<PublishGameCommand, GameChangeStateResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IGameRepository _gameRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PublishGameCommandHandler(ICurrentUserService currentUserService, IGameRepository gameRepository, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _gameRepository = gameRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GameChangeStateResponse> Handle(PublishGameCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Для публикации Игры нужно обладать правами Администратора");

            var game = await _gameRepository.GetByIdAsync(request.GameId, cancellationToken);

            if (game == null)
                throw new NotFoundException("Игра не найдена");

            game.Publish();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new GameChangeStateResponse
            {
                Success = true
            };
        }
    }
}

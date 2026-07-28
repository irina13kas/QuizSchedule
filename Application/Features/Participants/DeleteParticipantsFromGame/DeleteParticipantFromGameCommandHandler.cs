using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Games;
using Application.DTOs.Participants;
using AutoMapper;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Participants.DeleteParticipantsFromGame
{
    public class DeleteParticipantFromGameCommandHandler : IRequestHandler<DeleteParticipantFromGameCommand, GameResponseForParticipants>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameParticipantRepository _participantRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public DeleteParticipantFromGameCommandHandler(ICurrentUserService currentUserService, 
            IUnitOfWork unitOfWork, 
            IGameParticipantRepository participantRepository,
            IGameRepository gameRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _participantRepository = participantRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<GameResponseForParticipants> Handle(DeleteParticipantFromGameCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может удалять участников игры");

            var participant = await _participantRepository.GetByIdAsync(request.ParticipantId, cancellationToken);
            if (participant == null)
                throw new NotFoundException("Участника игры не существует");

            var game = await _gameRepository.GetByIdAsync(participant.GameId, cancellationToken);
            if(game == null)
                throw new NotFoundException("Игра не найдена");

            if (game.Status == GameStatus.Cancelled)
                throw new NotBusinessSuitableException("Нельзя удалять участников из отменённой игры");

            _participantRepository.Remove(participant);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var updateGame = await _gameRepository.GetByIdWithParticipantsAsync(participant.GameId,
                cancellationToken);

            return _mapper.Map<GameResponseForParticipants>(updateGame);
        }
    }
}

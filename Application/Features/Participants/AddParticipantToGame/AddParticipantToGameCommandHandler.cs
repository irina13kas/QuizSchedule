using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Games;
using Application.DTOs.Participants;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Participants.AddParticipantToGame
{
    public class AddParticipantToGameCommandHandler: IRequestHandler<AddParticipantToGameCommand, GameResponseForParticipants>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameParticipantRepository _participantRepository;
        private readonly ISmartRepository _smart;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public AddParticipantToGameCommandHandler(ICurrentUserService currentUserService, 
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

        public async Task<GameResponseForParticipants> Handle(AddParticipantToGameCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Добавлять Квизмена на Игру может только Админ!");

            var game = await _gameRepository.GetByIdAsync(request.GameId, cancellationToken);
            if (game == null)
                throw new NotFoundException("Игра не существует");
            if (game.GameStartTime < DateTime.Now)
                throw new NotBusinessSuitableException("Нельзя назначать Квизменов на уже прошедшую игру");

            var quizeman = await _quizmanRepository.GetByIdAsync(request.QuizmanId, cancellationToken);
            if (quizeman == null)
                throw new NotFoundException("Квизмен не существует");

            var quizemanAvailability = await _smart.IsQuizemanAvailableOnThisDate(request.QuizmanId,
                game.GameStartTime);
            if (!quizemanAvailability)
                throw new NotBusinessSuitableException($"Квизмен не может в эту дату {game.GameStartTime.Date}");

            var alreadyParticipantOnGame = await _participantRepository.AlreadyParticipantOnGameAsync(request.QuizmanId,
                game.GameStartTime, cancellationToken);

            if (!alreadyParticipantOnGame)
                throw new NotBusinessSuitableException("Квизмен уже числится на игре в это время");

            Enum.TryParse<ParticipantRole>(request.Role, out ParticipantRole role);

            var participant = new GameParticipant(
                request.GameId, 
                request.QuizmanId, 
                role, 
                request.IsFullShift, 
                request.IsActive);

            await _participantRepository.AddAsync(participant, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var updateGame = await _gameRepository.GetByIdWithParticipantsAsync(game.Id, cancellationToken);

           return _mapper.Map<GameResponseForParticipants>(updateGame);
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.ConfirmGameWork
{
    internal class ConfirmGameWorkCommandHandler : IRequestHandler<ConfirmGameWorkCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IGameRepository _gameRepository;
        private readonly IGameParticipantRepository _participantRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmGameWorkCommandHandler(ICurrentUserService currentUserService,
            IGameRepository gameRepository, IGameParticipantRepository participantRepository,
            IQuizmanRepository quizmanRepository, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _gameRepository = gameRepository;
            _participantRepository = participantRepository;
            _quizmanRepository = quizmanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ConfirmGameWorkCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может подтверждать отработанную смену");

            var game = await _gameRepository.GetByIdAsync(request.GameId, cancellationToken);
            if (game == null)
                throw new NotFoundException("Игра не существует");

            if (game.Status != GameStatus.Completed)
                throw new NotBusinessSuitableException("Игра еще не завершена");

            var participants = await _participantRepository.GetParticipantsByGameIdAsync(game.Id,
                cancellationToken);
            if (participants == null)
                throw new NotFoundException("Участников на игре не существует");

            var excludedIds = request.ExcludeQuizmanIds ?? new List<Guid>();
            var workedParticipants = participants
                .Where(p => !excludedIds.Contains(p.QuizmanId))
                .ToList();

            foreach(var wP in workedParticipants)
            {
                var quizman = await _quizmanRepository.GetByIdAsync(wP.QuizmanId, 
                    cancellationToken);

                if (quizman == null)
                    continue;

                quizman.AddShift();

            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

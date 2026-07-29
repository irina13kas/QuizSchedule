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

namespace Application.Features.Participants.ChangeShiftType
{
    public class ChangeShiftTypeCommandHandler : IRequestHandler<ChangeShiftTypeCommand,
        GameWithParticipantsResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IGameParticipantRepository _participantRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGameRepository _gameRepository;

        public ChangeShiftTypeCommandHandler(ICurrentUserService currentUserService,
            IGameParticipantRepository participantRepository,
            IUnitOfWork unitOfWork,
            IGameRepository gameRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _participantRepository = participantRepository;
            _unitOfWork = unitOfWork;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<GameWithParticipantsResponse> Handle(ChangeShiftTypeCommand request, 
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может менять полноту смены Квизмена");

            var participant = await _participantRepository.GetByIdAsync(request.ParticipantId, cancellationToken);

            if (request.IsFullShift != participant.FullShift)
            {
                participant.ChangeFullShift();
                _participantRepository.Update(participant);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var updateGame = await _gameRepository.GetByIdWithParticipantsAsync(participant.GameId, cancellationToken);
                return _mapper.Map<GameWithParticipantsResponse>(updateGame);
            }
            return _mapper.Map<GameWithParticipantsResponse>(participant.Game);
        }
    }
}





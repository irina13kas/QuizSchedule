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

namespace Application.Features.Participants.ChangeParticipantActivity
{
    public class ChangeParticipantActivityCommandHandler : IRequestHandler<ChangeParticipantActivityCommand, GameResponseForParticipants>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameParticipantRepository _participantRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public ChangeParticipantActivityCommandHandler(ICurrentUserService currentUserService, 
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

        public async Task<GameResponseForParticipants> Handle(ChangeParticipantActivityCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может менять активность Квизмена");

            var participant = await _participantRepository.GetByIdAsync(request.ParticipantId, cancellationToken);

            if (request.IsActive != participant.IsActive)
            {
                participant.ChangeActivity();
                _participantRepository.Update(participant);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var updateGame = await _gameRepository.GetByIdWithParticipantsAsync(participant.GameId, cancellationToken);
                return _mapper.Map<GameResponseForParticipants>(updateGame);
            }
            else
            {
                return _mapper.Map<GameResponseForParticipants>(participant.Game);
            }
            
        }
    }
}

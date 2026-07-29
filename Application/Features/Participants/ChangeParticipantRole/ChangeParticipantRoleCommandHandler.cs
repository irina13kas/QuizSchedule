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

namespace Application.Features.Participants.ChangeParticipantRole
{
    public class ChangeParticipantRoleCommandHandler : IRequestHandler<ChangeParticipantRoleCommand,
        GameWithParticipantsResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameParticipantRepository _participantRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public ChangeParticipantRoleCommandHandler(ICurrentUserService currentUserService, 
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

        public async Task<GameWithParticipantsResponse> Handle(ChangeParticipantRoleCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может менять роль");

            var participant = await _participantRepository.GetByIdAsync(request.ParticipantId, cancellationToken);

            if (participant == null)
                throw new NotFoundException("Нет такого участника игры");

            if (!Enum.TryParse<ParticipantRole>(request.RoleName, out ParticipantRole role))
                throw new NotBusinessSuitableException("Значение не входит в диапозон допустимых");

            if (request.RoleName != participant.Role.ToString())
            {
                participant.ChangeRole(role);
                _participantRepository.Update(participant);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                var updateGame = await _gameRepository.GetByIdWithParticipantsAsync(participant.GameId, cancellationToken);
                return _mapper.Map<GameWithParticipantsResponse>(updateGame);
            }
            return _mapper.Map<GameWithParticipantsResponse>(participant.Game);

        }
    }
}

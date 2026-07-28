using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Games;
using AutoMapper;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.GetGame
{
    public class GetGameQueryHandler : IRequestHandler<GetGameQuery, GameWithParticipantsResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GetGameQueryHandler(
            IGameRepository gameRepository, 
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<GameWithParticipantsResponse> Handle(
            GetGameQuery request, 
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var game = await _gameRepository.GetByIdWithParticipantsAsync(request.GameId, cancellationToken);

            return _mapper.Map<GameWithParticipantsResponse>(game);
        }
    }
}

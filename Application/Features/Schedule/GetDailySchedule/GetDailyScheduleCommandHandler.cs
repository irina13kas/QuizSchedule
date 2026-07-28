using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Games;
using Application.DTOs.Schedule;
using AutoMapper;
using MediatR;
using System.Globalization;

namespace Application.Features.Schedule.GetGamesList
{
    public class GetDailyScheduleCommandHandler : IRequestHandler<GetDailyScheduleCommand,
        DailyScheduleResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GetDailyScheduleCommandHandler(ICurrentUserService currentUserService, 
            IGameRepository gameRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<DailyScheduleResponse> Handle(GetDailyScheduleCommand request,
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var gameList = await _gameRepository.GetByDateWithParticipantsAsync(request.Date,
                cancellationToken);

            return new DailyScheduleResponse
            {
                Date = request.Date,
                DayOfWeek = request.Date.ToString("dddd", new CultureInfo("ru-RU")),
                Games = _mapper.Map<List<GameWithParticipantsResponse>>(gameList)
            };
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Schedule;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedule.GetWeekSchedule
{
    public class GetWeekScheduleCommandHandler : IRequestHandler<GetWeekScheduleCommand,
        ScheduleMatrixResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GetWeekScheduleCommandHandler(ICurrentUserService currentUserService, IGameRepository gameRepository, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<ScheduleMatrixResponse> Handle(GetWeekScheduleCommand request,
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var gameList = await _gameRepository.GetByDatePeriodWithParticipantsAsync(
                request.DateFrom,
                request.DateTo,
                cancellationToken);

            if(gameList == null)
                throw new NotFoundException("Игры на эту неделю не существуют");

            return new ScheduleMatrixResponse
            {
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                ScheduleForWeek = _mapper.Map<List<DailyScheduleResponse>>(gameList)
            };
        }
    }
}

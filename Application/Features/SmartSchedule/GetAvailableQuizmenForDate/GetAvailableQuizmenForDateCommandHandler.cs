using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Quizman;
using Application.DTOs.Smart;
using AutoMapper;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.GetAvailableQuizmen
{
    public class GetAvailableQuizmenForDateCommandHandler : IRequestHandler<GetAvailableQuizmenForDateCommand,
        AvailableQuizmenForDateResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ISmartRepository _scheduleRepository;
        private readonly IMapper _mapper;
        
        public GetAvailableQuizmenForDateCommandHandler(ICurrentUserService currentUserService,
            ISmartRepository scheduleRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        public async Task<AvailableQuizmenForDateResponse> Handle(GetAvailableQuizmenForDateCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может смотреть доступных квизменов");

            var availableQuizmen = await _scheduleRepository.GetAllAvailableQuizmenAsync(
                request.Date,
                cancellationToken);

            return new AvailableQuizmenForDateResponse
            {
                Date = DateOnly.FromDateTime(request.Date),
                AvailableQuizmen = _mapper.Map<List<QuizmanResponse>>(availableQuizmen)
            };
        }
    }
}

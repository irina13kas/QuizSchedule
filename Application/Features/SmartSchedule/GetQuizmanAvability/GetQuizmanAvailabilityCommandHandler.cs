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

namespace Application.Features.SmartSchedule.GetQuizmanAvability
{
    public class GetQuizmanAvailabilityCommandHandler : IRequestHandler<GetQuizmanAvailabilityCommand,
        AvailableDaysForQuizmanResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ISmartRepository _scheduleRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IMapper _mapper;

        public GetQuizmanAvailabilityCommandHandler(ICurrentUserService currentUserService, 
            ISmartRepository scheduleRepository, 
            IQuizmanRepository quizmanRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _scheduleRepository = scheduleRepository;
            _quizmanRepository = quizmanRepository;
            _mapper = mapper;
        }

        public async Task<AvailableDaysForQuizmanResponse> Handle(GetQuizmanAvailabilityCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может смотреть доступных квизменов");

            var quizmanAvailableDays = await _scheduleRepository.GetAvailableDatesForQuizmenAsync(
                request.QuizmanId,
                request.DateFrom,
                request.DateTo,
                cancellationToken);

            var quizman = await _quizmanRepository.GetByIdAsync(request.QuizmanId,
                cancellationToken);

            return new AvailableDaysForQuizmanResponse
            {
                QuizmanId = request.QuizmanId,
                QuizemanName = quizman.User.Name,
                AvailableDaysForQuizman = _mapper.Map<List<SmartItemResponse>>(quizmanAvailableDays)
            };
        }
    }
}

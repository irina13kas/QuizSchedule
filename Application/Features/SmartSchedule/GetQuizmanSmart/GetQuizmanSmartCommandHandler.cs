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

namespace Application.Features.SmartSchedule.GetQuizmanSmart
{
    public class GetQuizmanSmartCommandHandler : IRequestHandler<GetQuizmanSmartCommand,
        SmartDaysForQuizmanResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ISmartRepository _scheduleRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IMapper _mapper;

        public GetQuizmanSmartCommandHandler(ICurrentUserService currentUserService, 
            ISmartRepository scheduleRepository, 
            IQuizmanRepository quizmanRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _scheduleRepository = scheduleRepository;
            _quizmanRepository = quizmanRepository;
            _mapper = mapper;
        }

        public async Task<SmartDaysForQuizmanResponse> Handle(GetQuizmanSmartCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может смотреть доступные дни для квизмена");

            var quizman = await _quizmanRepository.GetByIdAsync(request.QuizmanId,
                cancellationToken);
            if (quizman != null)
                throw new NotFoundException("Квизмен не найден");
            SmartStatus? status = null;
            if (Enum.TryParse<SmartStatus>(request.Status, true, out SmartStatus st))
            {
                status = st;
            }
 
            var quizmanAvailableDays = await _scheduleRepository.GetSmartDatesForQuizmanByStatusAsync(
                request.QuizmanId,
                status,
                request.DateFrom,
                request.DateTo,
                cancellationToken);

            return new SmartDaysForQuizmanResponse
            {
                QuizmanId = request.QuizmanId,
                QuizemanName = quizman.User.Name,
                SmartDaysForQuizman = _mapper.Map<List<SmartItemResponse>>(quizmanAvailableDays)
            };
        }
    }
}

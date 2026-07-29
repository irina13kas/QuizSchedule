using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Smart;
using AutoMapper;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.GetMySmart
{
    public class GetMySmartCommandHandler : IRequestHandler<GetMySmartCommand,
        SmartDaysForQuizmanResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ISmartRepository _smartRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IMapper _mapper;

        public GetMySmartCommandHandler(
            ICurrentUserService currentUserService, 
            ISmartRepository smartRepository,
            IQuizmanRepository quizmanRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _smartRepository = smartRepository;
            _quizmanRepository = quizmanRepository;
            _mapper = mapper;
        }

        public async Task<SmartDaysForQuizmanResponse> Handle(GetMySmartCommand request, 
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Quizman.ToString())
                throw new ForbiddenException("Этот эндпоинт для Квизмена");

            var userId = _currentUserService.UserId ?? throw new UnauthorizedException();
            var quizman = await _quizmanRepository.GetByUserIdAsync(userId, cancellationToken);

            if (quizman == null)
                throw new NotFoundException("Квизмен не существует");

            var smart = await _smartRepository.GetSmartDatesForQuizmanAsync(quizman.Id, 
                request.DateFrom, request.DateTo, cancellationToken);

            return new SmartDaysForQuizmanResponse
            {
                QuizmanId = quizman.Id,
                QuizemanName = quizman.User.Name,
                SmartDaysForQuizman = _mapper.Map<List<SmartItemResponse>>(smart)
            };
        }
    }
}

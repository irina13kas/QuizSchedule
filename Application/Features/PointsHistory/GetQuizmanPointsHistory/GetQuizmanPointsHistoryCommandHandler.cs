using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Points;
using AutoMapper;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PointsHistory.GetQuizemanPointsHistory
{
    public class GetQuizmanPointsHistoryCommandHandler : IRequestHandler<GetQuizmanPointsHistoryCommand,
        QuimanPointsHistoryResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IPointsRepository _pointsRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IMapper _mapper;

        public GetQuizmanPointsHistoryCommandHandler(ICurrentUserService currentUserService,
            IPointsRepository pointsRepository,
            IQuizmanRepository quizmanRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _pointsRepository = pointsRepository;
            _quizmanRepository = quizmanRepository;
            _mapper = mapper;
        }

        public async Task<QuimanPointsHistoryResponse> Handle(GetQuizmanPointsHistoryCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может просматривать историю начисления баллов квизменов");

            var quizman = await _quizmanRepository.GetByIdAsync(request.QuizmanId);
            if (quizman == null)
                throw new NotFoundException("Квизмен не существует");

            var skip = (request.Page - 1) * request.PageSize;

            var totalCount = await _pointsRepository.CountByQuizmanAsync(request.QuizmanId,
                request.DateFrom, request.DateTo,
                cancellationToken);

            var points = await _pointsRepository.GetFilteredPointsHistoryAsync(quizman.Id,
                request.DateFrom, request.DateTo, skip, request.PageSize, cancellationToken);

            if (points == null)
                throw new NotFoundException("История очков не существует");

            return new QuimanPointsHistoryResponse
            {
                QuizmanId = quizman.Id,
                QuizmanName = quizman.User.Name,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                Points = _mapper.Map<List<PointsHistoryItemDTO>>(points)
            };
        }
    }
}

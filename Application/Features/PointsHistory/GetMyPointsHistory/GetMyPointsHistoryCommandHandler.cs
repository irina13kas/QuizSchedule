using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Points;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PointsHistory.GetMyPointsHistory
{
    public class GetMyPointsHistoryCommandHandler : IRequestHandler<GetMyPointsHistoryCommand,
        MyPointsHistoryResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IPointsRepository _pointsRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IMapper _mapper;

        public GetMyPointsHistoryCommandHandler(ICurrentUserService currentUserService, 
            IPointsRepository pointsRepository,
            IQuizmanRepository quizmanRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _pointsRepository = pointsRepository;
            _quizmanRepository = quizmanRepository;
            _mapper = mapper;
        }

        public async Task<MyPointsHistoryResponse> Handle(GetMyPointsHistoryCommand request, 
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId 
                ?? throw new UnauthorizedException();

            var quizman = await _quizmanRepository.GetByUserIdAsync(userId, cancellationToken);
            if (quizman == null)
                throw new NotFoundException("Квизмен не существует");

            int skip = (request.Page - 1) * request.PageSize;

            var pointsHistory = await _pointsRepository.GetFilteredPointsHistoryAsync(
                quizman.Id,
                request.DateFrom, 
                request.DateTo,
                skip,
                request.PageSize,
                cancellationToken);

            var totalCount = await _pointsRepository.CountByQuizmanAsync(
                quizman.Id,
                request.DateFrom,
                request.DateTo,
                cancellationToken);

            return new MyPointsHistoryResponse
            {
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                Points = _mapper.Map<List<PointsHistoryItemDTO>>(pointsHistory)
            };

        }
    }
}

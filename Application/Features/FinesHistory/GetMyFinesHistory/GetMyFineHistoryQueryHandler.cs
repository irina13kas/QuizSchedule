using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Fines;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinesHistory.GetAllFines
{
    public class GetMyFineHistoryQueryHandler: IRequestHandler<GetMyFineHistoryQuery, MyFinesHistoryResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IQuizmanRepository _quizemanRepository;
        private readonly IFineRepository _finesHistoryRepository;
        private readonly IMapper _mapper;

        public GetMyFineHistoryQueryHandler(
            ICurrentUserService currentUserService,
            IQuizmanRepository quizemanRepository,
            IFineRepository finesHistoryRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _quizemanRepository = quizemanRepository;
            _finesHistoryRepository = finesHistoryRepository;
            _mapper = mapper;
        }

        public async Task<MyFinesHistoryResponse> Handle(
            GetMyFineHistoryQuery request,
            CancellationToken cancellationToken)
        {

            var userId = _currentUserService.UserId
                ?? throw new ForbiddenException("Пользователь не авторизован");

            if (_currentUserService.Role != UserRole.Quizman.ToString())
                throw new ForbiddenException("Этот эндпоинт доступен только Квизменам");

            var quizeman = await _quizemanRepository.GetByUserIdAsync(userId, cancellationToken);

            if (quizeman == null)
                throw new NotFoundException(nameof(Quizman), userId);
            int skip = (request.Page - 1) * request.PageSize;
            var finesHistory = await _finesHistoryRepository.GetFineHistoryAsync(quizeman.Id, request.FromDate, request.ToDate,
                skip,
                request.PageSize,
                cancellationToken);

            var totalCount = await _finesHistoryRepository.CountByQuizemanAsync(quizeman.Id, request.FromDate,
                request.ToDate,
                cancellationToken);

            return new MyFinesHistoryResponse
            {
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                Fines = _mapper.Map<List<FinesHistoryItemDTO>>(finesHistory)
            };
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Replacements;
using Application.DTOs.ReplacementsSchedule;
using AutoMapper;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.GetReplacements
{
    public class GetMyReplacementsCommandHandler : IRequestHandler<GetMyReplacementsCommand,
        GetReplacementsResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IReplacementRepository _replacementRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IMapper _mapper;

        public GetMyReplacementsCommandHandler(ICurrentUserService currentUserService, 
            IReplacementRepository replacementRepository,
            IQuizmanRepository quizmanRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _replacementRepository = replacementRepository;
            _quizmanRepository = quizmanRepository;
            _mapper = mapper;
        }

        public async Task<GetReplacementsResponse> Handle(GetMyReplacementsCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Quizman.ToString())
                throw new ForbiddenException("Этот эндпоинт только для Квизменов");

            var userId = _currentUserService.UserId ?? default;

            var quizman = await _quizmanRepository.GetByUserIdAsync(userId,
                cancellationToken);

            int skip = (request.Page - 1)* request.PageSize;

            var totalCount = await _replacementRepository.CountByQuizmanAsync(quizman.Id,
                request.DateTo, cancellationToken);

            var replacements = await _replacementRepository.GetFilteredReplacementsAsync(
                request.DateFrom,
                request.DateTo,
                skip,
                request.PageSize,
                cancellationToken);

            if (replacements == null)
                throw new NotFoundException("Замены не найдены");

            return new GetReplacementsResponse
            {
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                Replacements = _mapper.Map<List<ReplacementResponse>>(replacements)
            };
        }
    }
}

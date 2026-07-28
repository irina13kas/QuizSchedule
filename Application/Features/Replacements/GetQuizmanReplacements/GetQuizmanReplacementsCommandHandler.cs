using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Replacements;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.GetQuizmanReplacements
{
    public class GetQuizmanReplacementsCommandHandler : IRequestHandler<GetQuizmanReplacementsCommand,
        GetReplacementsResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IReplacementRepository _replacementRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IMapper _mapper;

        public GetQuizmanReplacementsCommandHandler(ICurrentUserService currentUserService,
            IReplacementRepository replacementRepository,
            IQuizmanRepository quizmanRepository,
            IMapper mapper)
        {
            _replacementRepository = replacementRepository;
            _currentUserService = currentUserService;
            _quizmanRepository = quizmanRepository;
        }

        public async Task<GetReplacementsResponse> Handle(GetQuizmanReplacementsCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAuthenticated)
                throw new ForbiddenException("Пользователь не аутентифицирован");

            var userId = _currentUserService.UserId ?? default;

            var quizman = await _quizmanRepository.GetByUserIdAsync(userId,
                cancellationToken);

            int skip = (request.Page - 1) * request.PageSize;

            var totalCount = await _replacementRepository.CountReplacementsAsync(
                request.DateFrom, request.DateTo, cancellationToken);

            var replacements = await _replacementRepository.GetFilteredReplacementsAsync(
                request.DateFrom,
                request.DateTo,
                skip,
                request.PageSize,
                cancellationToken);

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

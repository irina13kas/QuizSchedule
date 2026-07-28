using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Quizman;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.GetQuizmenList
{
    public class GetQuizmenListQueryHandler : IRequestHandler<GetQuizmenListQuery,
        QuizmenListResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IMapper _mapper;

        public GetQuizmenListQueryHandler(
            ICurrentUserService currentUserService, 
            IQuizmanRepository quizmanRepository, 
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _quizmanRepository = quizmanRepository;
            _mapper = mapper;
        }

        public async Task<QuizmenListResponse> Handle(GetQuizmenListQuery request,
            CancellationToken cancellationToken)
        {
            if(!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var quizmen = await _quizmanRepository.GetAllAsync(cancellationToken);
            if (request.IsActive.HasValue)
                quizmen = quizmen.Where(q => q.User.IsDeleted == request.IsActive);

            return new QuizmenListResponse
            {
                Quizmen = _mapper.Map<List<QuizmanResponse>>(quizmen)
            };
        }
    }
}

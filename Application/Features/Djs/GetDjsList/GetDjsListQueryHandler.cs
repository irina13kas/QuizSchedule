using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Bars;
using Application.DTOs.Djs;
using Application.Features.Bars.GetBarsList;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Djs.GetDjsList
{
    public class GetDjsListQueryHandler : IRequestHandler<GetDjsListQuery, DjsListResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Dj> _djsRepository;
        private readonly IMapper _mapper;

        public GetDjsListQueryHandler(
            ICurrentUserService currentUserService, 
            IRepository<Dj> djsRepository, 
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _djsRepository = djsRepository;
            _mapper = mapper;
        }

        public async Task<DjsListResponse> Handle(GetDjsListQuery query,
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var djs = await _djsRepository.GetAllAsync(cancellationToken);
            if (query.IsActive.HasValue)
                djs = djs.Where(x => x.IsActive == query.IsActive);

            return new DjsListResponse
            {
                Djs = _mapper.Map<List<DjResponse>>(djs)
            };
        }
    }
}

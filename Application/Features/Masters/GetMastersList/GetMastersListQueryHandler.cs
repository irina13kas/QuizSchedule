using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Djs;
using Application.DTOs.Masters;
using Application.Features.Djs.GetDjsList;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Masters.GetMastersList
{
    public class GetMastersListQueryHandler : IRequestHandler<GetMastersListQuery, 
        MastersListResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Master> _mastersRepository;
        private readonly IMapper _mapper;

        public GetMastersListQueryHandler(
            ICurrentUserService currentUserService, 
            IRepository<Master> mastersRepository, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _mastersRepository = mastersRepository;
            _mapper = mapper;
        }

        public async Task<MastersListResponse> Handle(GetMastersListQuery query,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var masters = await _mastersRepository.GetAllAsync(cancellationToken);
            if (query.IsActive.HasValue)
                masters = masters.Where(x => x.IsActive == query.IsActive);

            return new MastersListResponse
            {
                Masters = _mapper.Map<List<MasterResponse>>(masters)
            };
        }
    }
}

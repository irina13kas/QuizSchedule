using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Bars;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bars.GetBarsList
{
    public class GetBarsListQueryHandler: IRequestHandler<GetBarsListQuery, BarListResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Bar> _barsRepository;
        private readonly IMapper _mapper;

        public GetBarsListQueryHandler(
            ICurrentUserService currentUserService, 
            IRepository<Bar> barsRepository, 
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _barsRepository = barsRepository;
            _mapper = mapper;
        }

        public async Task<BarListResponse> Handle(GetBarsListQuery query,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var bars = await _barsRepository.GetAllAsync(cancellationToken);
            if(query.IsActive.HasValue)
                bars = bars.Where(x => x.IsActive == query.IsActive);

            return new BarListResponse
            {
                Bars = _mapper.Map<List<BarResponse>>(bars)
            };
        }
    }
}

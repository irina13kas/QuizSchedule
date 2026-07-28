using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Bars;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bars.GetBar
{
    public class GetBarQueryHandler : IRequestHandler<GetBarQuery, BarResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Bar> _barsRepository;
        
        public GetBarQueryHandler(ICurrentUserService currentUserService,
            IRepository<Bar> barsRepository)
        {
            _currentUserService = currentUserService;
            _barsRepository = barsRepository;
        }

        public async Task<BarResponse> Handle(GetBarQuery query, CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var bar = await _barsRepository.GetByIdAsync(query.BarId, cancellationToken);
            if(bar == null || !bar.IsActive)
                throw new NotFoundException("Бар не существует");

            return new BarResponse
            {
                BarId = bar.Id,
                BarName = bar.Name,
                Address = bar.Address,
                Capacity = bar.Capacity,
                IsActive = bar.IsActive,
            };
        }
    }
}

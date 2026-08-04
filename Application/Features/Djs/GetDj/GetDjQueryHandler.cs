using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Bars;
using Application.DTOs.Djs;
using Application.Features.Bars.GetBar;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Djs.GetDj
{
    public class GetDjQueryHandler : IRequestHandler<GetDjQuery, DjResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Dj> _djsRepository;

        public GetDjQueryHandler(
            ICurrentUserService currentUserService, 
            IRepository<Dj> djsRepository)
        {
            _currentUserService = currentUserService;
            _djsRepository = djsRepository;
        }

        public async Task<DjResponse> Handle(GetDjQuery query, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var dj = await _djsRepository.GetByIdAsync(query.DjId, cancellationToken);
            if (dj == null || !dj.IsActive)
                throw new NotFoundException("Dj не существует");

            return new DjResponse
            {
                DjId = dj.Id,
                Name = dj.Name,
                IsActive = dj.IsActive,
            };
        }
    }
}

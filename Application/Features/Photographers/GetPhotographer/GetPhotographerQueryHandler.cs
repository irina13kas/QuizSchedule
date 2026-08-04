using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Masters;
using Application.DTOs.Photographers;
using Application.Features.Masters.GetMaster;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Photographers.GetPhotographer
{
    public class GetPhotographerQueryHandler : IRequestHandler<GetPhotographerQuery,
        PhotographerResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Photographer> _photosRepository;

        public GetPhotographerQueryHandler(
            ICurrentUserService currentUserService, 
            IRepository<Photographer> photosRepository)
        {
            _currentUserService = currentUserService;
            _photosRepository = photosRepository;
        }

        public async Task<PhotographerResponse> Handle(GetPhotographerQuery query,
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var photographer = await _photosRepository.GetByIdAsync(query.PhotographerId, cancellationToken);
            if (photographer == null || !photographer.IsActive)
                throw new NotFoundException("Фотограф не существует");

            return new PhotographerResponse
            {
                PhotographerId = photographer.Id,
                Name = photographer.Name,
                IsActive = photographer.IsActive,
            };
        }
    }
}

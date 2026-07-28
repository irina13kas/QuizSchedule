using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Masters;
using Application.DTOs.Photographers;
using Application.Features.Masters.GetMastersList;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Photographers.GetPhotographersList
{
    public class GetPhotographersListQueryHandler : IRequestHandler<GetPhotographersListQuery,
        PhotographersListResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Photographer> _photosRepository;
        private readonly IMapper _mapper;

        public GetPhotographersListQueryHandler(
            ICurrentUserService currentUserService, 
            IRepository<Photographer> photosRepository, 
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _photosRepository = photosRepository;
            _mapper = mapper;
        }

        public async Task<PhotographersListResponse> Handle(GetPhotographersListQuery query,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var photographers = await _photosRepository.GetAllAsync(cancellationToken);
            if (query.IsActive.HasValue)
                photographers = photographers.Where(x => x.IsActive == query.IsActive);

            return new PhotographersListResponse
            {
                Photographers = _mapper.Map<List<PhotographerResponse>>(photographers)
            };
        }
    }
}

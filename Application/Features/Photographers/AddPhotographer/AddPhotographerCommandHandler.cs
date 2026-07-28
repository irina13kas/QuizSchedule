using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Masters;
using Application.DTOs.Photographers;
using Application.Features.Masters.AddMaster;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Photographers.AddPhotographer
{
    public class AddPhotographerCommandHandler : IRequestHandler<AddPhotographerCommand,
        PhotographerResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Photographer> _photosRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddPhotographerCommandHandler(
            ICurrentUserService currentUserService, 
            IRepository<Photographer> photosRepository, 
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _photosRepository = photosRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PhotographerResponse> Handle(AddPhotographerCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может создавать новых Ведущих");

            var photographer = new Photographer(request.Name);

            await _photosRepository.AddAsync(photographer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new PhotographerResponse
            {
                PhotographerId = photographer.Id,
                Name = photographer.Name,
                IsActive = photographer.IsActive,
            };
        }
    }
}

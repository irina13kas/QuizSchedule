using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Masters;
using Application.DTOs.Photographers;
using Application.Features.Masters.UpdateMaster;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Photographers.UpdatePhotographer
{
    public class UpdatePhotographerCommandHandler : IRequestHandler<UpdatePhotographerCommand,
        PhotographerResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Photographer> _photosRepository;

        public UpdatePhotographerCommandHandler(
            ICurrentUserService currentUserService, 
            IUnitOfWork unitOfWork, 
            IRepository<Photographer> photosRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _photosRepository = photosRepository;
        }

        public async Task<PhotographerResponse> Handle(UpdatePhotographerCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может менять информацию о Фотографе");

            var photographer = await _photosRepository.GetByIdAsync(request.PhotographerId, cancellationToken);
            if (photographer == null || !photographer.IsActive)
                throw new NotFoundException("Фотографа не существует");

            photographer.UpdateInfo(request.NewName);
            _photosRepository.Update(photographer);

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

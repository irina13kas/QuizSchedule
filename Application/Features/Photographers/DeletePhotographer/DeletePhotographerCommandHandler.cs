using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Photographers;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;

namespace Application.Features.Photographers.DeletePhotographer
{
    public class DeletePhotographerCommandHandler : IRequestHandler<DeletePhotographerCommand,
        ChangeStatePhotographerResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Photographer> _photosRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePhotographerCommandHandler(ICurrentUserService currentUserService,
            IRepository<Photographer> photosRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _photosRepository = photosRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ChangeStatePhotographerResponse> Handle(DeletePhotographerCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может удалять Фотографа");

            var photographer = await _photosRepository.GetByIdAsync(request.PhotographerId, cancellationToken);
            if (photographer == null)
                throw new NotFoundException("Фотограф не существует");

            photographer.ChangeActivity();
            _photosRepository.Update(photographer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ChangeStatePhotographerResponse
            {
                Success = true
            };
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Profiles;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.UpdatePhoto
{
    public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand, UpdatePhotoResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePhotoCommandHandler(
            ICurrentUserService currentUserService, 
            IRepository<User> userRepository, 
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdatePhotoResponse> Handle(
            UpdatePhotoCommand request, 
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? 
                throw new UnauthorizedException();

            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (user == null || user.IsDeleted)
                throw new NotFoundException("Пользователь не существует");

            user.UpdatePhoto(request.PhotoUrl);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdatePhotoResponse
            {
                PhotoUrl = user.PhotoUrl,
            };
        }
    }
}

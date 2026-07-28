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

namespace Application.Features.Profiles.UpdateMyAdminProfile
{
    public class UpdateMyAdminProfileCommandHandler : IRequestHandler<UpdateMyAdminProfileCommand, 
        AdminProfileResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMyAdminProfileCommandHandler(ICurrentUserService currentUserService, 
            IAdminRepository adminRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AdminProfileResponse> Handle(UpdateMyAdminProfileCommand request, 
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ??
                throw new UnauthorizedException();

            var admin = await _adminRepository.GetByUserIdAsync(userId, cancellationToken);
            if (admin == null)
                throw new NotFoundException("Админ не существует");

            if (admin.User.Id != _currentUserService.UserId)
                throw new ForbiddenException("Вы не можете модифицировать не свой профиль");

            if (admin.User.VkUrl.Equals(request.VkUrl))
                throw new ExistInDBException("Админ с таким Вк уже существует");

            admin.User.UpdateContactInfo(request.AdminName, request.VkUrl, request.BirthDay, request.PhotoUrl);

            if(request.DaysOff != null)
                admin.UpdateDaysOff(request.DaysOff);

            _adminRepository.Update(admin);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AdminProfileResponse
            {
                Id = admin.Id,
                Name = admin.User.Name,
                BirthDay = admin.User.BirthDay,
                Photo = admin.User.PhotoUrl,
                DaysOff = admin.DaysOff,
                VkUrl = admin.User.VkUrl
            };
        }
    }
}


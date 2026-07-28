using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Profiles;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.GetAdminProfile
{
    public class GetAdminProfileQueryHandler : IRequestHandler<GetAdminProfileQuery, AdminProfileResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Admin> _adminRepository;

        public GetAdminProfileQueryHandler(ICurrentUserService currentUserService, IRepository<Admin> adminRepository, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _adminRepository = adminRepository;
        }

        public async Task<AdminProfileResponse> Handle(GetAdminProfileQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var admin = await _adminRepository.GetByIdAsync(request.AdminId);
            if (admin == null)
                throw new NotFoundException("Администратора не существует");

            return new AdminProfileResponse
            {
                Id = admin.Id,
                Name = admin.User.Name,
                BirthDay = admin.User.BirthDay,
                VkUrl = admin.User.VkUrl,
                DaysOff = admin.DaysOff,
                Photo = admin.User.PhotoUrl
            };
        }
    }
}

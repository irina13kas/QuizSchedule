using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Auth;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.CompleteFirstLogin
{
    public class CompleteFirstLoginCommandHandler : IRequestHandler<CompleteFirstLoginCommand, CompleteFirstLoginResponse>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public CompleteFirstLoginCommandHandler(
            IRepository<User> userRepository, 
            IUnitOfWork unitOfWork, 
            ICurrentUserService currentUserService,
            IAdminRepository adminRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<CompleteFirstLoginResponse> Handle(CompleteFirstLoginCommand request, CancellationToken cancellationToken)
        {
            if(!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
                throw new NotFoundException("Пользователь не найден");

            if (user.VkUrl.Equals(request.VkUrl))
                throw new ExistInDBException("Пользователь с таким Вк уже существует");

            user.UpdateContactInfo(request.Name, request.VkUrl, request.BirthDay, request.PhotoUrl);
            user.CompleteFirstEnter();

            await _unitOfWork.SaveChangesAsync();

            return new CompleteFirstLoginResponse
            {
                Name = user.Name,
                PhotoUrl = user.PhotoUrl,
                VkUrl = user.VkUrl,
                BirthDay = user.BirthDay,
            };
        }
    }
}

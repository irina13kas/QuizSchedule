using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Auth;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, AuthChangeStateResponse>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(IRepository<User> userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<AuthChangeStateResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
                throw new UnauthorizedException();

            var user = await _userRepository.GetByIdAsync(userId.Value, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), userId.Value);

            var oldPasswordValid = _passwordHasher.Verify(request.OldPassword, user.PasswordHash);

            if (!oldPasswordValid)
                throw new ForbiddenException("Неверный старый пароль");

            var passwordHash = _passwordHasher.Hash(request.NewPassword);

            user.UpdatePassword(passwordHash);

            await _unitOfWork.SaveChangesAsync();

            return new AuthChangeStateResponse
            {
                Success = true
            };
        }
    }
}

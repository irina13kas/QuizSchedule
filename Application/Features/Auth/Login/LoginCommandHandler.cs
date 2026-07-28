using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Auth;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Login
{
    public class LoginCommandHandler: IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(
            IRepository<User> userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<LoginResponse> Handle(
            LoginCommand request, 
            CancellationToken cancellationToken)
        {
            var user = (await _userRepository
                .GetAllAsync(cancellationToken))
                .FirstOrDefault(u => u.Login == request.Login && !u.IsDeleted);

            if (user == null)
                throw new NotFoundException("Неверный логин или пароль");

            var isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
                throw new ForbiddenException("Неверный пароль");

            var token = _jwtTokenGenerator.GenerateAccessToken(user);

            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken(
                user.Id,
                refreshToken,
                DateTime.UtcNow.AddDays(15)
                );

            await _refreshTokenRepository.AddAsync(newRefreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new LoginResponse
            {
                AccessToken = token.Token,
                RefreshToken = refreshToken,
                UserId = user.Id,
                Role = user.Role.ToString(),
                AccessTokenExpiresAt = token.ExpiresAt
            };
        }

    }
}

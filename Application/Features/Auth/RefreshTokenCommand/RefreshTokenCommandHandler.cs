using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Auth;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.RefreshTokenCommand
{
    public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommand, LoginResponse>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
            
        public RefreshTokenCommandHandler(
            IRepository<User> userRepository, 
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken,
                cancellationToken);
            if (storedRefreshToken == null || !storedRefreshToken.IsActive)
                throw new ForbiddenException("RefreshToken не валиден");

            var principal = _jwtTokenGenerator.GetPrincipalFromExpiredToken(
                request.Token);
            if(principal == null)
                throw new ForbiddenException("AccessToken не валиден");

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
                throw new ForbiddenException("RefreshToken не принадлежит этому пользователю");

            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (user == null || user.IsDeleted)
                throw new NotFoundException("Пользователь не существует");

            var newAccessToken = _jwtTokenGenerator.GenerateAccessToken(user);
            var newRefreshTokenString = _jwtTokenGenerator.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken(
                userId,
                newRefreshTokenString,
                DateTime.UtcNow.AddDays(15));

            storedRefreshToken.Revoke(newRefreshToken.Id.ToString());

            await _refreshTokenRepository.AddAsync(newRefreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new LoginResponse
            {
                AccessToken = newAccessToken.Token,
                RefreshToken = newRefreshTokenString,
                UserId = userId,
                Role = user.Role.ToString(),
                AccessTokenExpiresAt = newAccessToken.ExpiresAt,
            };
        }
    }
}

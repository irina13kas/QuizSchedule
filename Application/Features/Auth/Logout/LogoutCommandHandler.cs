using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, AuthChangeStateResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogoutCommandHandler(
            ICurrentUserService currentUserService,
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthChangeStateResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ??
              throw new UnauthorizedException();

            var clientIp = _currentUserService.ClientIp;

            await _refreshTokenRepository.
                RevokeAllByUserAsync(
                userId,
                clientIp,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthChangeStateResponse
            {
                Success = true,
            };
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Notifications;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.RegisterPushToken
{
    public class RegisterPushTokenCommandHandler : IRequestHandler<RegisterPushTokenCommand, NotificationChangeStateResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IPushRepository _pushTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterPushTokenCommandHandler(ICurrentUserService currentUserService, IPushRepository pushTokenRepository, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _pushTokenRepository = pushTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<NotificationChangeStateResponse> Handle(RegisterPushTokenCommand request,
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            if (!Enum.TryParse<PlatformType>(request.Platform, ignoreCase: true, out PlatformType platform))
                throw new NotBusinessSuitableException("Тип платформы не соответствует разрешенным");

            var pushToken = await _pushTokenRepository.GetTokenAsync(request.Token, cancellationToken);

            if (pushToken != null)
            {
                if (pushToken.UserId == _currentUserService.UserId)
                {
                    if (!pushToken.IsActive)
                        pushToken.Activate();
                }
                else
                    throw new ForbiddenException("Этот токен уже занят другим пользователем");                 
            }
            else
            {
                var newToken = new PushToken(_currentUserService.UserId.Value, request.Token, platform);
                await _pushTokenRepository.AddAsync(newToken, cancellationToken);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new NotificationChangeStateResponse
            {
                Successful = true,
            };
        }

    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.RemovePushToken
{
    public class RemovePushTokenCommandHandler : IRequestHandler<
        RemovePushTokenCommand, 
        NotificationChangeStateResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IPushRepository _pushTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemovePushTokenCommandHandler(
            ICurrentUserService currentUserService, 
            IPushRepository pushRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _pushTokenRepository = pushRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<NotificationChangeStateResponse> Handle(
            RemovePushTokenCommand request, 
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var pushToken = await _pushTokenRepository.GetTokenAsync(
                request.Token, 
                cancellationToken);

            if (pushToken != null)
            {

                if (pushToken.UserId != _currentUserService.UserId)
                {
                    throw new ForbiddenException("Нельзя удалять чужой токен");
                }
                pushToken.Deactivate();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return new NotificationChangeStateResponse
            {
                Successful = true,
            };
        }
    }
}

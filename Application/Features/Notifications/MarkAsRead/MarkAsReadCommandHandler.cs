using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.MarkAsRead
{
    public class MarkAsReadCommandHandler : IRequestHandler<MarkAsReadCommand, NotificationChangeStateResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkAsReadCommandHandler(ICurrentUserService currentUserService,
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<NotificationChangeStateResponse> Handle(MarkAsReadCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var notification = await _notificationRepository.GetByIdAsync(request.NotificationId);

            if (notification.UserId != _currentUserService.UserId)
                throw new ForbiddenException("Нельзя изменять уведомления чужого пользователя");

            if (!notification.IsRead)
            {
                notification.MarkAsRead();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return new NotificationChangeStateResponse
            {
                Successful = true
            };
        }
    }
}

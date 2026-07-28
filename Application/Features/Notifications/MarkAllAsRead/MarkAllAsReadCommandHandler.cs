using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.MarkAllAsRead
{
    public class MarkAllAsReadCommandHandler : IRequestHandler<MarkAllAsReadCommand, MarkAllAsReadResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkAllAsReadCommandHandler(
            ICurrentUserService currentUserService, 
            INotificationRepository notificationRepository, 
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<MarkAllAsReadResponse> Handle(
            MarkAllAsReadCommand request, 
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var userId = _currentUserService.UserId;

            var notifications = await _notificationRepository.GetAllNotificationsByUserIdWithUnreadStatusAsync(userId.Value, cancellationToken);

            foreach(var n in notifications)
            {
                n.MarkAsRead();
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new MarkAllAsReadResponse { 
                MarkedCount = notifications.Count
            };
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Notifications;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.GetUnreadedCount
{
    public class GetUnreadedCountCommandHandler : IRequestHandler<GetUnreadedCountCommand, UnreadCountResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly INotificationRepository _notificationRepository;
        private readonly IRepository<User> _userRepository;

        public GetUnreadedCountCommandHandler(
            ICurrentUserService currentUserService, 
            INotificationRepository notificationRepository,
            IRepository<User> userRepository)
        {
            _currentUserService = currentUserService;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        public async Task<UnreadCountResponse> Handle(GetUnreadedCountCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var userId = _currentUserService.UserId;

            var user = await _userRepository.GetByIdAsync(userId.Value, cancellationToken);
            if (user == null || user.IsDeleted)
                throw new NotFoundException("Пользователь не найден");

            var unreadCount = await _notificationRepository.CountNotificationsAsync(userId.Value,
                request.FromDate,
                request.ToDate,
                false,
                cancellationToken);

            return new UnreadCountResponse {
                UnreadCount = unreadCount,
            };
        }
    }
}

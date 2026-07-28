using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Notifications;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.GetNotificationsQuery
{
    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, NotificationListResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public GetNotificationsQueryHandler(ICurrentUserService currentUserService,  
            INotificationRepository notificationRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<NotificationListResponse> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var userId = _currentUserService.UserId;

            if(userId == Guid.Empty)
                throw new NotFoundException("Пользователь не найден");

            var skip = (request.Page - 1) * request.PageSize;

            var notifications = await _notificationRepository.GetAllNotificationsByUserIdAsync(userId.Value, 
                request.FromDate,
                request.ToDate,
                request.IsRead,
                request.Type,
                skip,
                request.PageSize,
                cancellationToken);

            var totalCount = await _notificationRepository.CountNotificationsAsync(userId.Value,
                request.FromDate, 
                request.ToDate,
                null,
                cancellationToken);

            var unreadNotifications = await _notificationRepository.CountNotificationsAsync(
                userId.Value,
                request.FromDate, 
                request.ToDate,
                false,
                cancellationToken);

            return new NotificationListResponse
            {

                TotalCount = totalCount,
                UnreadCount = unreadNotifications,
                NotificationDTOs = _mapper.Map<List<NotificationDTO>>(notifications),
                Page = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}

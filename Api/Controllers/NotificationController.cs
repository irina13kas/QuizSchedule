using Application.DTOs.Notifications;
using Application.Features.Notifications.GetNotificationsQuery;
using Application.Features.Notifications.GetUnreadedCount;
using Application.Features.Notifications.MarkAllAsRead;
using Application.Features.Notifications.MarkAsRead;
using Application.Features.Notifications.RegisterPushToken;
using Application.Features.Notifications.RemovePushToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("notifications")]
        [Authorize]
        [ProducesResponseType(typeof(NotificationListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<NotificationListResponse> GetNotification(
            [FromBody] GetNotificationsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("unread-count")]
        [Authorize]
        [ProducesResponseType(typeof(UnreadCountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<UnreadCountResponse> GetUnreadedCount(GetUnreadedCountCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("read-all")]
        [Authorize]
        [ProducesResponseType(typeof(MarkAllAsReadResponse), StatusCodes.Status200OK)]
        public async Task<MarkAllAsReadResponse> MarkAllAsRead()
        {
            return await _mediator.Send(new MarkAllAsReadCommand());
        }

        [HttpPost("{notificationId}/read")]
        [Authorize]
        [ProducesResponseType(typeof(NotificationChangeStateResponse), StatusCodes.Status200OK)]
        public async Task<NotificationChangeStateResponse> MarkAsRead(Guid notificationId)
        {
            return await _mediator.Send(new MarkAsReadCommand { NotificationId = notificationId });
        }

        [HttpPost("push-token")]
        [Authorize]
        [ProducesResponseType(typeof(NotificationChangeStateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<NotificationChangeStateResponse> RegisterPushToken(
            RegisterPushTokenCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("push-token")]
        [Authorize]
        [ProducesResponseType(typeof(NotificationChangeStateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<NotificationChangeStateResponse> RemovePushToken(RemovePushTokenCommand 
            command)
        {
            return await _mediator.Send(command);
        }
    }
}

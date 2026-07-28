using Application.DTOs.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.RemovePushToken
{
    public class RemovePushTokenCommand : IRequest<NotificationChangeStateResponse>
    {
        public string Token { get; set; } = string.Empty;
    }
}

using Application.DTOs.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.RegisterPushToken
{
    public class RegisterPushTokenCommand : IRequest<NotificationChangeStateResponse>
    {
        public string Token { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
    }
}

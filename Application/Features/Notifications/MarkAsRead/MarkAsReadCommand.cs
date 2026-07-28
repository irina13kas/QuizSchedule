using Application.DTOs.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.MarkAsRead
{
    public class MarkAsReadCommand : IRequest<NotificationChangeStateResponse>
    {
        public Guid NotificationId { get; set; }
    }
}

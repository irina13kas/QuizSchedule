using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities
{
    public class Notification: BaseEntity
    {
        public Guid UserId { get; private set; }
        public Guid SenderId { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public string Title { get; private set; } = string.Empty;
        public NotificationType Type { get; private set; }
        public bool IsRead { get; private set; } = false;

        public virtual User User { get; private set; } = null!;
        public virtual User Sender { get; private set; } = null!;

        protected Notification() { }

        public Notification(Guid userId, Guid senderId, string message, string title, NotificationType type)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Укажите получателя сообщения");

            if (senderId == Guid.Empty)
                throw new ArgumentException("Укажите отправителя сообщения");

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Сообщение обязательно к заполнению");

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Заголовок обязателен к заполнению");
            UserId = userId;
            SenderId = senderId;
            Message = message;
            Title = title;
            Type = type;
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}

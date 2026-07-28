using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PushToken : BaseEntity
    {
        public Guid UserId { get; private set; }
        public bool IsActive { get; private set; } = true;
        public string Token { get; private set; }
        public PlatformType Platform { get; private set; } = PlatformType.Android;

        public virtual User User { get; private set; } = null!; 

        protected PushToken() { }

        public PushToken(Guid userId, PlatformType platform)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Укажите ID пользователя для получения сообщения");

            UserId = userId;
            Platform = platform;
        }

        public PushToken(Guid userId, string token, PlatformType platform)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Укажите ID пользователя для получения сообщения");

            UserId = userId;
            Token = token;
            Platform = platform;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }
}

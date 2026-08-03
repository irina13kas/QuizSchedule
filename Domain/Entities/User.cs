using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User: BaseEntity
    {
        public string Login { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string? PhotoUrl { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? VkUrl { get; private set; }
        public DateTime? BirthDay { get; private set; }
        public bool IsFirstEnterFinished { get; private set; } = false;
        public bool IsDeleted { get; private set; } = false;
        public UserRole Role { get; private set; } = UserRole.Quizman;

        public virtual Admin? Admin { get; private set; } = null!;
        public virtual Quizman? Quizman { get; private set; } = null!;

        public virtual ICollection<Notification> Notifications { get; private set; }
        public virtual ICollection<PushToken> PushTokens { get; private set; }

        protected User()
        {
            Notifications = new List<Notification>();
            PushTokens = new List<PushToken>();
        }

        public User(string login, string password, string name, UserRole role, 
            string vkUrl,
            string photoUrl = default, DateTime birthDay = default): this()
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Логин обязателен", nameof(login));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Пароль обязателен", nameof(password));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя обязательно", nameof(name));

            if (string.IsNullOrWhiteSpace(vkUrl))
                throw new ArgumentException("ВК обязателен", nameof(vkUrl));

            Login = login;
            PasswordHash = password;
            Name = name;
            PhotoUrl = photoUrl;
            BirthDay = birthDay;
            Role = role;
            VkUrl = vkUrl;
        }

        public void CompleteFirstEnter()
        {
            IsFirstEnterFinished = true;
        }

        public void DeleteProfile()
        {
            IsDeleted = true;
        }

        public void RestoreProfile()
        {
            IsDeleted = false;
        }

        public void UpdatePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
        }

        public void UpdateContactInfo(string? name, string? vkLink, DateTime? birthDay, string? photo)
        {
            if(name != null)
                Name = name;

            if (vkLink != null)
                VkUrl = vkLink;

            if (birthDay != null)
                BirthDay = birthDay;

            if(photo != null)
                PhotoUrl = photo;
        }

        public void UpdatePhoto(string photoUrl)
        {
            PhotoUrl = photoUrl;
        }

        public bool IsAdmin => Role == UserRole.Admin;
        public bool IsQuizeman => Role == UserRole.Quizman;    

    }
}

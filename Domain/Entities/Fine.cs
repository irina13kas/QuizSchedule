using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities
{
    public class Fine: BaseEntity
    {
        public Guid QuizmanId { get; private set; }
        public Guid AdminId { get; private set; }
        public int Amount { get; private set; }
        public string Comment { get; private set; } = string.Empty;
        public Guid? GameId { get; private set; }
        public bool IsClosed { get; private set; } = false;

        public virtual Quizman Quizman { get; private set; }
        public virtual Admin Admin { get; private set; }
        public virtual Game? Game { get; private set; }

        protected Fine() { }

        public Fine(Guid quizemanId, Guid adminId, int amout, string comment, Guid? gameId)
        {
            if (quizemanId == Guid.Empty)
                throw new ArgumentException("Укажите квизмена для начисления штрафа");

            if (adminId == Guid.Empty)
                throw new ArgumentException("Укажите администратора для начисления штрафа");

            if (amout <= 0)
                throw new ArgumentException("Укажите количество начисленных штрафов");

            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentException("Требуется указать причину начисления/удаления карточек");

            QuizmanId = quizemanId;
            AdminId = adminId;
            GameId = gameId;
            Amount = amout;
            Comment = comment;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(Guid? quizemanId, Guid? adminId, int? amount, string? comment, Guid? gameId)
        {
            if (quizemanId.HasValue)
                QuizmanId = quizemanId.Value;

            if (adminId.HasValue)
                AdminId = adminId.Value;

            if (gameId.HasValue)
                GameId = gameId.Value;

            if (amount.Value <= 0)
                throw new ArgumentException("Укажите количество начисленных штрафов");
            if(amount.HasValue)
                Amount = amount.Value;

            if (!string.IsNullOrWhiteSpace(comment))
                Comment = comment;
        }

        public void ChangeStatus()
        {
            IsClosed = !IsClosed;
        }
    }
}

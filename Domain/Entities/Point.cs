using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Point : BaseEntity
    {
        public Guid QuizmanId { get; private set; }
        public Guid AdminId { get; private set; }
        public int Amount { get; private set; }
        public string Comment { get; private set; } = string.Empty;
        public Guid? GameId { get; private set; }

        public virtual Quizman Quizman { get; private set; }
        public virtual Admin Admin { get; private set; }
        public virtual Game? Game { get; private set; }

        protected Point() { }

        public Point(Guid quizemanId, Guid adminId, int capacity, string coment, Guid? gameId = default)
        {
            if (quizemanId == Guid.Empty)
                throw new ArgumentException("Укажите квизмена для начисления баллов");

            if (adminId == Guid.Empty)
                throw new ArgumentException("Укажите администратора для начисления баллов");
            
            if (gameId == Guid.Empty)
                throw new ArgumentException("Укажите игру, на которой получены баллы");

            if (capacity <= 0)
                throw new ArgumentException("Укажите количество начисленных баллов");

            if (string.IsNullOrWhiteSpace(coment))
                throw new ArgumentException("Требуется указать причину начисления/списания баллов");

            QuizmanId = quizemanId;
            AdminId = adminId;
            GameId = gameId;
            Amount = capacity;
            Comment = coment;
        }
    }
}

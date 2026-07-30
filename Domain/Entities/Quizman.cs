using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities
{
    public class Quizman :  BaseEntity
    {
        public Guid UserId { get; private set; }
        public int Points { get; private set; } = 0;
        public int Fines { get; private set; } = 0;
        public int WorkShift { get; private set; } = 3;
        
        public virtual User User { get; private set; } = null!;

        public virtual ICollection<Smart> Smart { get; private set; }
        public virtual ICollection<GameParticipant> Games { get; private set; }
        public virtual ICollection<Replacement> Replacements { get; private set; }
        public virtual ICollection<Fine> FinesHistory { get; private set; }
        public virtual ICollection<Point> PointsHistory { get; private set; }

        protected Quizman() {

            Smart = new List<Smart>();
            Games = new List<GameParticipant>();
            Replacements = new List<Replacement>();
            PointsHistory = new List<Point>();
            FinesHistory = new List<Fine>();
        }

        public Quizman(Guid userId) : this()
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException("userId не может быть нулевым");

            UserId = userId;
        }

        public void AddPoints(Point points)
        {
            if (points.Amount < 0)
                throw new ArgumentOutOfRangeException("Нельзя начислить отрицательное количество очков");
            if (points == null)
                throw new ArgumentException("Укажите запись о зачислении баллов");
            Points += points.Amount;
            PointsHistory.Add(points);
        }

        public void RemovePoints(Point points) 
        {
            if (points == null)
                throw new ArgumentNullException("Укажите запись о зачислении баллов");
            if (Points - points.Amount >= 0)
            {
                Points -= points.Amount;
                PointsHistory.Remove(points);
            }
            else
                throw new ArgumentException("Нельзя снять баллы, так как их колчиество меньше суммы снятия");
        }

        public void DiscardPoints(Point points)
        {
            if (points == null)
                throw new ArgumentNullException("Укажите запись о зачислении баллов");
            if (Points - points.Amount >= 0)
            {
                Points -= points.Amount;
                PointsHistory.Add(points);
            }
            else
                throw new ArgumentException("Нельзя снять баллы, так как их количество меньше суммы снятия");
        }

        public void AddFine(Fine fine)
        {
            if (fine.Amount < 0)
                throw new ArgumentOutOfRangeException("Нельзя начислить отрицательное количество карточек");
            if (fine == null)
                throw new ArgumentNullException("Укажите запись о штрафе");
            Fines += fine.Amount;
            FinesHistory.Add(fine);
        }

        public void RemoveFine(Fine fine)
        {
            if(fine == null)
                throw new ArgumentNullException("Укажите запись о штрафе");
            if (Fines - fine.Amount >= 0)
            {
                Fines -= fine.Amount;
                FinesHistory.Remove(fine);
            }
            else
                throw new ArgumentException("У Квизмена больше нет карточек, которые можно убрать");
        }

        public void AddReplacement(Replacement replacement)
        {
            Replacements.Add(replacement);
        }

        public void RemoveReplacement(DateOnly date)
        {
            var quizeman = Replacements.FirstOrDefault(p => p.Date == date);
            Replacements.Remove(quizeman);
        }

        public void AddAvailable(Smart available)
        {
            Smart.Add(available);
        }

        public void AddToGame(GameParticipant participant)
        {
            Games.Add(participant);
        }

        public void AddShift()
        {
            WorkShift++;
        }

        public bool IsFinesExceeded()
        {
            return Fines > 5;
        }
    }
}

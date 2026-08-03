using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Admin : BaseEntity
    {
        public Guid UserId { get; private set; }
        public string? DaysOff { get; private set; }
        
        public virtual User User { get; private set; } = null!;

        public ICollection<Game> CreatedGames { get; private set; }
        public ICollection<Game> WorkedGames { get; private set; }
        public ICollection<Replacement> TakenReplacements { get; private set; }
        public ICollection<Fine> GivenFines { get; private set; }
        public ICollection<Point> GivenPoints { get; private set; }

        protected Admin()
        {
            CreatedGames = new List<Game>();
            TakenReplacements = new List<Replacement>();
            GivenFines = new List<Fine>();
            GivenPoints = new List<Point>();
            WorkedGames = new List<Game>();
        }

        public Admin(Guid userId, string? daysOff = null) : this()
        {
            if(userId == Guid.Empty)
                throw new ArgumentNullException("userId не может быть нулевым");
            if (daysOff != null)
                DaysOff = daysOff;
            UserId = userId;
        }

        public void AddGame(Game game)
        {
            CreatedGames.Add(game);
        }

        public void TakeReplacement(Replacement rep)
        {
            TakenReplacements.Add(rep);  
        }

        public void TakeBackReplacement(Guid quizemanId)
        {
            var quizeman = TakenReplacements.FirstOrDefault(r => r.QuizemanId == quizemanId);
            if (quizeman !=null)
                TakenReplacements.Remove(quizeman);

        }

        public void UpdateDaysOff(string? daysOff)
        {
            DaysOff = daysOff;
        }
    }
}

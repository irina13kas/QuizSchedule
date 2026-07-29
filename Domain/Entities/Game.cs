using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public GameStatus Status { get; private set; } = GameStatus.Draft;
        public Guid? ResponsibleAdminId { get; private set; }
        public Guid? BarId { get; private set; }
        public string? Partner { get; private set; }
        public Guid CreateByAdminId { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public DateTime GameStartTime { get; private set; }
        public DateTime WorkStartTime { get; private set; }
        public Guid? MasterId { get; private set; }    
        public Guid? DjId { get; private set; }
        public Guid? PhotographerId { get; private set; }

        public virtual Admin? ResponsibleAdmin { get; private set; }
        public virtual Bar? Bar { get; private set; }
        public virtual Admin AdminCreator { get; private set; } = null!;
        public virtual Master? Master { get; private set; }
        public virtual Dj? Dj { get; private set; }
        public virtual Photographer? Photographer { get; private set; }

        public virtual ICollection<GameParticipant> Participants { get; private set; }

        protected Game()
        {
            Participants = new List<GameParticipant>();
        }

        public Game(string name, Guid adminId, Guid barId, string partner, 
            Guid adminCreator, DateTime gameTime, DateTime startTime,
            Guid masterId, Guid djId, Guid photographerId) : this()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя обязательно к заполнению");

            if (adminId == Guid.Empty)
                throw new ArgumentException("Укажите администратора для проведения игры");

            if (barId == Guid.Empty)
                throw new ArgumentException("Укажите бар для проведения игры");

            if (adminCreator == Guid.Empty)
                throw new ArgumentException("Укажите создателя игры");

            if (DjId == Guid.Empty)
                throw new ArgumentException("Укажите Dj игры");
            if (MasterId == Guid.Empty)
                throw new ArgumentException("Укажите Ведущего игры");
            if (PhotographerId == Guid.Empty)
                throw new ArgumentException("Укажите Фотографа игры");
            Name = name;
            ResponsibleAdminId = adminId;
            BarId = barId;
            Partner = partner;
            CreateByAdminId = adminCreator;
            DjId = djId;
            MasterId = masterId;
            PhotographerId = photographerId;
            CreatedAt = DateTime.UtcNow;
            if (gameTime <= DateTime.UtcNow)
                GameStartTime = gameTime;
            else
                throw new Exception("Время и дата игры меньше текущей");

            if (startTime <= DateTime.UtcNow || startTime > GameStartTime)
                WorkStartTime = startTime;
            else
                throw new Exception("Время и дата начала смены меньше текущей или больше время начала игры");
        }

        public void Publish()
        {
            if (!MasterId.HasValue || !DjId.HasValue
                || !PhotographerId.HasValue || !ResponsibleAdminId.HasValue)
                throw new ArgumentException("Нельзя опубликовать игру без назначенного старшего персонала");

            Status = GameStatus.Available;
            UpdateDate = DateTime.UtcNow;

        }

        public void Cancel()
        {
            Status = GameStatus.Cancelled;
            UpdateDate = DateTime.UtcNow;
        }

        public void AddQuizeman(GameParticipant participant)
        {
            Participants.Add(participant);
        }

        public void RemoveQuizeman(Guid quizeman)
        {
            var partipacipant = Participants.FirstOrDefault(p => p.QuizmanId == quizeman);
            if (partipacipant != null)
                Participants.Remove(partipacipant);
        }

        public void Update(string name, GameStatus status, Guid responsibleAdmin,
            Guid barId, Guid masterId, Guid djId, Guid photographerId, DateTime gameStartTime,
            DateTime workStartTime, string parther)
        {
            if(Enum.IsDefined(typeof(GameStatus), status))
                Status = status;

            if(!string.IsNullOrWhiteSpace(Name))
                Name = name;

            ResponsibleAdminId = responsibleAdmin;
            BarId = barId;
            MasterId = masterId;
            DjId = djId;
            PhotographerId = photographerId;
            if (GameStartTime > WorkStartTime)
                GameStartTime = gameStartTime;
            else
                throw new ArgumentException("Время начала игры не может быть раньше начала смены");
            WorkStartTime = workStartTime;
            Partner = parther;
            UpdateDate = DateTime.UtcNow;

        }
    }
}

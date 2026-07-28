using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GameParticipant: BaseEntity
    {
        public Guid GameId { get; private set; }
        public Guid QuizmanId { get; private set; }
        public ParticipantRole Role { get; private set; } = ParticipantRole.None;
        public bool IsActive { get; private set; }
        public bool FullShift { get; private set; }

        public virtual Game Game { get; private set; } = null!;
        public virtual Quizman Quizman { get; private set; } = null!;

        protected GameParticipant() { }
        public GameParticipant(Guid gameId, Guid quizemanId, ParticipantRole role, bool fullShift, bool isActive)
        {
            if (gameId == Guid.Empty)
                throw new ArgumentException("Укажите игру, куда назначаете квизмена");

            if (quizemanId == Guid.Empty)
                throw new ArgumentException("Укажите квизмена, которого хотите добавить на игру");

            GameId = gameId;
            QuizmanId = quizemanId;
            Role = role;
            FullShift = fullShift;
            IsActive = isActive;
        }

        public void ChangeRole(ParticipantRole newRole)
        {
            if (Role != newRole) 
                Role = newRole;
        }

        public void ChangeFullShift()
        {
            FullShift = !FullShift;
        }

        public void ChangeActivity()
        {
            IsActive = !IsActive;
        }
    }
}

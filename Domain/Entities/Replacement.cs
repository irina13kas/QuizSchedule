using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Replacement : BaseEntity
    {
        public DateOnly Date { get; private set; }
        public Guid QuizemanId { get; private set; }
        public string? Comment { get; private set; }
        public bool IsFullShift { get; private set; }
        public ReplacementStatus Status { get; private set; } = ReplacementStatus.Wait;
        public Guid? TakenAdminId { get; private set; }

        public virtual Quizman Quizeman { get; private set; } = null!;
        public virtual Admin? TakenAdmin { get; private set; }

        protected Replacement() { }

        public Replacement(DateTime date, Guid quizeman, string comment, bool isFullShift)
        {
            if (QuizemanId == Guid.Empty)
                throw new ArgumentException("Квизмен не может быть пустым ", nameof(QuizemanId));

            Date = DateOnly.FromDateTime(date);
            QuizemanId = quizeman;
            Comment = comment;
            IsFullShift = isFullShift;
        }

        public void Take(Guid admin)
        {
            if(admin == Guid.Empty)
                throw new ArgumentException("Админ не может быть пустым ",nameof(admin));
            if (Status == ReplacementStatus.Wait)
            {
                TakenAdminId = admin;
                Status = ReplacementStatus.Taken;
            }
            else
                throw new InvalidOperationException("Квизмен уже выпущен на игру");

        }
        public void Update(string? comment, bool isFullShift)
        {
            if (comment != null)
            {
                if (Status != ReplacementStatus.Taken)
                    Comment = comment;
                else
                    throw new Exception("Нельзя менять комментарий после того, как Вас уже взяли на игру");
            }
            if (IsFullShift != isFullShift)
                IsFullShift = isFullShift;
        }
    }
}

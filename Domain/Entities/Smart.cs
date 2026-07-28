using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Smart : BaseEntity
    {
        public DateOnly Date { get; private set; }
        public Guid QuizmanId { get; private set; }
        public SmartStatus Status { get; private set; }
        public string? Comment { get; private set; }

        public virtual Quizman Quizman { get; private set; } = null!;

        protected Smart() { }

        public Smart(DateOnly date, SmartStatus status, Guid quizemanId, string comment)
        {
            if (quizemanId == Guid.Empty)
                throw new ArgumentNullException("quizemanId не может быть нулевым");

            Date = date;
            QuizmanId = quizemanId;
            Status = status;
            Comment = comment;
        }

        public void Update(SmartStatus status, string? comment)
        {
            Status = status;
            Comment = comment ?? string.Empty;
        }

        public void ChangeStatus(SmartStatus status)
        {
            Status = status;
        }

        public void UpdateComment(string comment)
        {
            Comment = comment;
        }
    }
}

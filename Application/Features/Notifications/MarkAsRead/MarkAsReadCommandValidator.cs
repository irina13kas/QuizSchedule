using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.MarkAsRead
{
    public class MarkAsReadCommandValidator : AbstractValidator<MarkAsReadCommand>
    {
        public MarkAsReadCommandValidator() {
            RuleFor(x => x.NotificationId)
                .NotEmpty().WithMessage("Id уведомления обязателен");
        }
    }
}

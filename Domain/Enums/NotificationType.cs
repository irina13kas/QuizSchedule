using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum NotificationType
    {
        [Description("Расписание готово")]
        SchedulePublished = 0,
        [Description("Чилл сегодня")]
        Chill = 1,
        [Description("Отмена игры")]
        GameCancelled = 2,
        [Description("Выходи вместо")]
        ReplaceQuizeman = 3,
        [Description("Ждем на игре")]
        WaitOnGame = 4
    }
}

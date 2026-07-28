using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ParticipantRole
    {
        None = 0,
        [Description("Старший")]
        Senior = 1,
        [Description("Кассир")]
        Cashier = 2,
        [Description("Ввод")]
        Entering = 3,
        [Description("Сбор")]
        Collector = 4,
    }
}

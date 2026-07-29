using Application.DTOs.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DailySchedule
    {
        public DateTime Date { get; private set; }
        public string DayOfWeek { get; private set; } = string.Empty;
        public ICollection<Game> Games { get; private set; }

        protected DailySchedule() {
            Games = new List<Game>();
        }

        public DailySchedule(DateTime date): this(){
            Date = date.Date;
            DayOfWeek = date.ToString("dddd", new CultureInfo("ru-RU"));
        }
        public DailySchedule(DateTime date, Game game) : this()
        {
            Date = date.Date;
            DayOfWeek = date.ToString("dddd", new CultureInfo("ru-RU"));

            if (game == null)
                throw new ArgumentNullException("Нельзя добавить пустую игру");

            Games.Add(game);
        }

        public void AddGame(Game game)
        {
            if (game == null)
                throw new ArgumentNullException("Нельзя добавить пустую игру");

            Games.Add(game);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Data.App.Models.Calendars
{
    public class Calendar
    {
        public DateTime Date { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Quarter { get; set; }

        public string MonthName { get; set; }
        public int DayOfYear { get; set; }
        public int DayOfWeek { get; set; }
        public string DayName { get; set; }

    }
}

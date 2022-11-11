using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericHelpersLibrary
{
    public static class GetWeekNumberFromDateClass
    {
        public static int GetWeekNumber(DateTime date)
        {
            var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday) date = date.AddDays(3);

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }

        public static int GetWeekNumber(DateOnly date)
        {
            var d = new DateTime(date.Year, date.Month, date.Day);
            var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(d);
            if (day is >= DayOfWeek.Monday and <= DayOfWeek.Wednesday) d = d.AddDays(3);

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(d, CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }

        public static DateTime GetFirstDayOfTheWeek(int week, int year)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);
            int firstWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(firstMonday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            if (firstWeek <= 1)
            {
                week -= 1;
            }
            DateTime result = firstMonday.AddDays(week * 7);
            return result;
        }

        public static DateTime GetLastDayOfTheWeek(int week, int year)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);
            int firstWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(firstMonday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            if (firstWeek <= 1)
            {
                week -= 1;
            }
            DateTime result = firstMonday.AddDays(week * 7);
            return result.AddDays(7).AddSeconds(-1);
        }
    }
}

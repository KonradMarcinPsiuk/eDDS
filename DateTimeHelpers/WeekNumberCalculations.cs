using System.Globalization;

namespace DateTimeHelpers;

public static class WeekNumberCalculations
{
    public static DateTime GetFirstDayOfTheWeek(int week, int year)
    {
        var jan1 = new DateTime(year, 1, 1);
        var daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
        var firstMonday = jan1.AddDays(daysOffset);
        var firstWeek =
            CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(firstMonday, CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        if (firstWeek <= 1) week -= 1;
        var result = firstMonday.AddDays(week * 7);
        return result;
    }

    public static DateTime GetLastDayOfTheWeek(int week, int year)
    {
        var jan1 = new DateTime(year, 1, 1);
        var daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
        var firstMonday = jan1.AddDays(daysOffset);
        var firstWeek =
            CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(firstMonday, CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        if (firstWeek <= 1) week -= 1;
        var result = firstMonday.AddDays(week * 7);
        return result.AddDays(7).AddSeconds(-1);
    }
}
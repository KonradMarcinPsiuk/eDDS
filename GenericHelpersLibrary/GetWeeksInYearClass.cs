using System.Globalization;

namespace GenericHelpersLibrary
{
    public static class GetWeeksInYearClass
    {
        public static int GetWeeksInYear(int year)
        {
            if (year <= 0) return 0;
            var dfi = DateTimeFormatInfo.CurrentInfo;
            var date1 = new DateTime(year, 12, 31);
            var cal = dfi.Calendar;
            return cal.GetWeekOfYear(date1, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        public static int[] GetWeeksInYearToArray(this int numberOfWeeks)
        {
            return Enumerable.Range(1, numberOfWeeks).ToArray();
        }
     
    }
}

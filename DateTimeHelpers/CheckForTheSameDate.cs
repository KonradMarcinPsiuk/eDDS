namespace DateTimeHelpers;

public static class CheckForTheSameDate
{
    public static bool CheckDate(DateTime date1, DateTime date2)
    {
        return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day;
    }
}
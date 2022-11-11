using Extensions;
namespace DailyResults.Queries;

public static class Queries
{
    public static string GetResultForLineAndDate(DateTime date, int lineId) =>
        $"DailyResults/GetResultForLineAndDate/{date.ToApiFormat()}/{lineId}";
    public static string SaveDailyResult => "DailyResults/SaveDailyResult";
    public static string CheckIfResultForLineAndDateExists(DateTime date, int lineId) =>
        $"DailyResults/CheckIfResultForLineAndDateExists/{date.ToApiFormat()}/{lineId}";
    public static string GetResultsForDateAndDepartment(DateTime date, int deptId) =>
        $"DailyResults/GetResultsForDateAndDepartment/{date.ToApiFormat()}/{deptId}";

}
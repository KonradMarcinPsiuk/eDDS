namespace DailyPlanner.Queries;

public static class Queries
{
    public static string SavePlan()
    {
        return "DailyPlanner/SaveDailyPlan/";
    }

    public static string GetDailyPlan(int planId)
    {
        return $"DailyPlanner/GetDailyPlan/{planId}";
    }

    public static string GetPlanTypes()
    {
        return "DailyPlanType/GetDailyPlanTypes";
    }

    public static string GetPlans(int lineId, int? year, int? week)
    {
        return $"DailyPlanner/GetPlans/{lineId}/{year}/{week}";
    }

    public static string DeletePlan(int planId)

    {
        return $"DailyPlanner/DeletePlan/{planId}";
    }
}

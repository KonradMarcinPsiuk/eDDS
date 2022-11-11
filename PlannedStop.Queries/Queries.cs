namespace PlannedStop.Queries;

public static class Queries
{
    public static string SavePmTasks => "PlannedStop/SavePmTasks";
    public static string SaveCilTasks => "PlannedStop/SaveCilTasks";
    public static string SaveClTasks => "PlannedStop/SaveClTasks";
    public static string SaveOtherTasks => "PlannedStop/SaveOtherTasks";

    public static string SavePmTask => "PlannedStop/SavePmTask";
    public static string SaveCilTask => "PlannedStop/SaveCilTask";
    public static string SaveClTask => "PlannedStop/SaveClTask";
    public static string SaveOtherTask => "PlannedStop/SaveOtherTask";

    public static string GetPmTask(Guid taskId) => $"PlannedStop/GetPmTask/{taskId}";
    public static string GetCilTask(Guid taskId) => $"PlannedStop/GetCilTask/{taskId}";
    public static string GetClTask(Guid taskId) => $"PlannedStop/GetClTask/{taskId}";
    public static string GetOtherTask(Guid taskId) => $"PlannedStop/GetOtherTask/{taskId}";

    public static string GetPmTasksForLine(int lineId, bool openOnly) => $"PlannedStop/GetPmTasksForLine/{lineId}/{openOnly}";
    public static string GetCilTasksForLine(int lineId, bool openOnly) => $"PlannedStop/GetCilTasksForLine/{lineId}/{openOnly}";
    public static string GetClTasksForLine(int lineId, bool openOnly) => $"PlannedStop/GetClTasksForLine/{lineId}/{openOnly}";
    public static string GetOtherTasksForLine(int lineId, bool openOnly) => $"PlannedStop/GetOtherTasksForLine/{lineId}/{openOnly}";

    public static string DeletePmTask => $"PlannedStop/DeletePmTask";
    public static string DeleteCilTask => $"PlannedStop/DeleteCilTask";
    public static string DeleteClTask => $"PlannedStop/DeleteClTask";
    public static string DeleteOtherTask => $"PlannedStop/DeleteOtherTask";


}
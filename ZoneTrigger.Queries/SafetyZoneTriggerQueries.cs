namespace ZoneTrigger.Queries;

public static class SafetyZoneTriggerQueries
{
    public static string GetSafetyZoneTrigger(int reportId) => $"SafetyZoneTrigger/GetSafetyZoneTrigger/{reportId}";
    public static string GetSafetyZoneTriggerQuestion(int? deptId = null) =>
        $"SafetyZoneTrigger/GetSafetyZoneTriggerQuestion/{deptId = null}";
    public static string SaveSafetyZoneTriggerQuestion => "SafetyZoneTrigger/SaveSafetyZoneTriggerQuestion";
    public static string SaveSafetyZoneTrigger => "SafetyZoneTrigger/SaveSafetyZoneTrigger";
    public static string AddDepartmentToQuestion => "SafetyZoneTrigger/AddDepartmentToQuestion";
    public static string DeleteDepartmentLink => "SafetyZoneTrigger/DeleteDepartmentLink";
    public static string GetSafetyQuestion(int id) => $"SafetyZoneTrigger/GetSafetyQuestion/{id}";
}
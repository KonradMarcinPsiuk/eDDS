using Extensions;

namespace DailyTrigger.Queries
{
    public static class Queries
    {
        public static string GetQuestions => "DailyTrigger/GetQuestions";
        public static string GetQuestion(int questionId) => $"DailyTrigger/GetQuestion/{questionId}";
        public static string SaveQuestion => "DailyTrigger/SaveQuestion";
        public static string GetTriggersForLineAndDate(int lineId, DateTime date) =>
            $"DailyTrigger/GetTriggersForLineAndDate/{lineId}/{date.ToApiFormat()}";
        public static string SaveProductionLineDailyTriggerLink => "DailyTrigger/SaveProductionLineDailyTriggerLink";
        public static string DeleteProductionLineDailyTriggerLink =>
            "DailyTrigger/DeleteProductionLineDailyTriggerLink";
        public static string GetNumberOfQuestionsAssignedToTheLine(int lineId) =>
            $"DailyTrigger/GetNumberOfQuestionsAssignedToTheLine/{lineId}";
        public static string CheckIfAnswersForLineAndDateExists(DateTime date, int lineId) =>
            $"DailyTrigger/CheckIfAnswersForLineAndDateExists/{date.ToApiFormat()}/{lineId}";
        public static string SaveAnswer => "DailyTrigger/SaveAnswer";
        public static string SaveAnswers => "DailyTrigger/SaveAnswers";
        public static string GetAnswersForDepartmentAndDate(DateTime date, int deptId) =>
            $"DailyTrigger/GetAnswersForDepartmentAndDate/{date.ToApiFormat()}/{deptId}";
    }
}

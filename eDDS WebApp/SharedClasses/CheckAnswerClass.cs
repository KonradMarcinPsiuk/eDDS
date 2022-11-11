namespace eDDS.WebTool.SharedClasses;

public static class CheckAnswerClass
{
    public static string CheckAnswer(int? answer, int? target, int type)
    {
        if (answer == null || target == null) return "";
        if (answer == target) return "Safe";
        if (type == 1 && answer == 2) return "Warning";
        return "Risk";
    }
}
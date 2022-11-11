namespace Defects.Queries;

public static class Queries
{
    public static string GetDefect(Guid defectId)
    {
        return $"Defects/GetDefect/{defectId}";
    }

    public static string SaveDefect()
    {
        return "Defects/SaveDefect/";
    }

    public static string GetDefects(int? lineId, int? areaId, bool closedOnly)
    {
        return $"Defects/GetDefects/lineId={lineId}&areaId={areaId}&closedOnly={closedOnly}";
    }

    public static string GetDefectsForLine(int lineId,string  status)
    {
        return $"Defects/GetDefectsForLine/{lineId}/{status}";
    }

    public static string GetSubtypes()
    {
        return "Defects/GetSubtypes";
    }

    public static string CloseDefect()
    {
        return "Defects/CloseDefect";
    }

    public static string OpenDefect()
    {
        return "Defects/OpenDefect";
    }

    public static string DeleteDefect(Guid defectId) => $"Defects/DeleteDefect/{defectId}";
}
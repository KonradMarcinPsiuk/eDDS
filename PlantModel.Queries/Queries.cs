namespace PlantModel.Queries;

public static class Queries
{
    public static string GetPlants()
    {
        return "PlantModel/GetPlants";
    }

    public static string GetValueStreamsForPlant(int plantId)
    {
        return $"PlantModel/GetValueStreamsForPlant/{plantId}";
    }

    public static string GetDepartmentsForValueStream(int vsId)
    {
        return $"PlantModel/GetDepartmentsForValueStream/{vsId}";
    }
    
    public static string GetDepartmentsForPlant(int plantId)
    {
        return $"PlantModel/GetDepartmentsForPlant/{plantId}";
    }

    public static string GetLinesForDepartment(int departmentId)
    {
        return $"PlantModel/GetLinesForDepartment/{departmentId}";
    }

    public static string GetLineAreasForLine(int lineId)
    {
        return $"PlantModel/GetLineAreasForLine/{lineId}";
    }
}
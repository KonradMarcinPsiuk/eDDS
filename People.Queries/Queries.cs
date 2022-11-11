namespace People.Queries;

public class Queries
{
    public static string GetPeople(int? plantId, int? valueStreamId, int? deptId)
    {
        if(deptId!=null)
            return  $"People/GetPeople/Department/{deptId}";
        
        if(valueStreamId!=null)
            return $"People/GetPeople/ValueStream/{valueStreamId}";
        
        return $"People/GetPeople/Plant/{plantId}";
    }

    public static string SavePerson()
    {
        return "People/SavePerson";
    }

    public static string DeletePerson(int personId)
    {
        return $"People/DeletePerson/{personId}";
    }
}
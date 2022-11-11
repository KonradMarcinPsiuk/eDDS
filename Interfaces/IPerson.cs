namespace Interfaces;

public interface IPerson<TDept> where TDept : class, IDepartment
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsRealPerson { get; set; }
    public string? EmailAddress { get; set; }
    public List<TDept> Departments { get; set; }
    public string FullName { get; }
}
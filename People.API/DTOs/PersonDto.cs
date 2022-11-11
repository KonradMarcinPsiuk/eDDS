using Interfaces;

namespace People.API.DTOs;

public class PersonDto:IPerson<DepartmentDto>
{
 
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public bool IsRealPerson { get; set; }
    public string? EmailAddress { get; set; }
    public List<DepartmentDto> Departments { get; set; } = null!;
    public string FullName => $"{LastName} {FirstName}";
}
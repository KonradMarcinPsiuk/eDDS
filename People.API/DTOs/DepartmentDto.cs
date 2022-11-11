using Interfaces;

namespace People.API.DTOs;

public class DepartmentDto:IDepartment
{
    public int Id { get; set; }
    public string? DepartmentName { get; set; }
    public int ValueStreamId { get; set; }
}
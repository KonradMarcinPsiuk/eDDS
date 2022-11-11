namespace Interfaces;

public interface IDepartment
{
    public int Id { get; set; }
    public string? DepartmentName { get; set; }
    public int ValueStreamId { get; set; }
}
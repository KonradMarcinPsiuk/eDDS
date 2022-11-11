namespace PlantModel.API.DTOs;

public class DepartmentQueryDto
{
    public int Id { get; set; }
    public string? DepartmentName { get; set; }
    public int ValueStreamId { get; set; }
    public ValueStreamQueryDto ValueStream { get; set; } = null!;
}
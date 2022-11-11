namespace PlantModel.API.DTOs;

public class ValueStreamQueryDto
{
    public int Id { get; set; }
    public string ValueStreamName { get; set; } = null!;
    public int? PlantId { get; set; }
    public virtual PlantQueryDto Plant { get; set; } = null!;
}
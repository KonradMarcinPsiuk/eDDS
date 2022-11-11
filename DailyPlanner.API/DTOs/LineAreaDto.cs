namespace DailyPlanner.API.DTOs;

public class LineAreaDto
{
    public int Id { get; set; }
    public int ProductionLineId { get; set; }
    public string AreaName { get; set; } = null!;
    public bool IsDeleted { get; set; }

}
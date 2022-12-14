using DataLayer.Models;

namespace PlannedStop.API.DTOs;

public class CilTaskQueryDto
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? Action { get; set; }
    public int Status { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }
    public int LineAreaId { get; set; }
    public bool IsDeleted { get; set; }
    public int Priority { get; set; }

    public LineAreaQueryDto LineArea { get; set; } = null!;
}
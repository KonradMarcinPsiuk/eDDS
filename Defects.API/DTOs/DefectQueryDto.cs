namespace Defects.API.DTOs;

public class DefectQueryDto
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? Action { get; set; }
    public int Status { get; set; }
    public int SubTypeId { get; set; }

    public int? OwnerId { get; set; }
    public PersonQueryDto? Owner { get; set; }

    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }
    public int LineAreaId { get; set; }
    public LineAreaQueryDto? LineArea { get; set; } = null!;
    public bool FoundDuringCil { get; set; }
    public bool IsDeleted { get; set; }
    public bool PmHelpNeeded { get; set; }
    public string? PmHelpText { get; set; }
    public int Priority { get; set; }
}
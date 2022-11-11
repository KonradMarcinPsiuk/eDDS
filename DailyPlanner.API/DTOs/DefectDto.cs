namespace DailyPlanner.API.DTOs;

public class DefectDto
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? Action { get; set; }
    public int Status { get; set; }
    public int SubTypeId { get; set; }
    public int? OwnerId { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }
    public int LineAreaId { get; set; }
    public LineAreaDto LineArea { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public int Priority { get; set; }
    
    //Defect
    public bool FoundDuringCil { get; set; }
    public bool PmHelpNeeded { get; set; }
    public string? PmHelpText { get; set; }

}
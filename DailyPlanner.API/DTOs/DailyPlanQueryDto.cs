using DataLayer.Models;

namespace DailyPlanner.API.DTOs;

public class DailyPlanQueryDto
{   
    public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public int ProductionLineId { get; set; }
    public ProductionLineDto ProductionLine { get; set; } = null!;
    public string? Comment { get; set; }
    public int DailyPlanTypeId { get; set; }
    public bool IsDeleted { get; set; }
    public List<DailyPlanDefectTaskQueryDto> DailyPlanDefectTasks { get; set; } = null!;
    public List<DailyPlanCilTaskQueryDto> DailyPlanCilTasks { get; set; } = null!;
    public List<DailyPlanClTaskQueryDto> DailyPlanClTasks { get; set; } = null!;
    public List<DailyPlanOtherTaskQueryDto> DailyPlanOtherTasks { get; set; } = null!;
    public List<DailyPlanPmTaskQueryDto> DailyPlanPmTasks { get; set; } = null!;
}
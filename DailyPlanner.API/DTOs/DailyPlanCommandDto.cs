using DataLayer.Models;

namespace DailyPlanner.API.DTOs;

public class DailyPlanCommandDto
{
 public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public int ProductionLineId { get; set; }
    public string? Comment { get; set; }
    public int DailyPlanTypeId { get; set; }
    public bool IsDeleted { get; set; }
    public List<DailyPlanDefectTaskCommandDto> DailyPlanDefectTasks { get; set; } = null!;
    public List<DailyPlanCilTaskCommandDto> DailyPlanCilTasks { get; set; } = null!;
    public List<DailyPlanClTaskCommandDto> DailyPlanClTasks { get; set; } = null!;
    public List<DailyPlanOtherTaskCommandDto> DailyPlanOtherTasks { get; set; } = null!;
    public List<DailyPlanPmTaskCommandDto> DailyPlanPmTasks { get; set; } = null!;
}
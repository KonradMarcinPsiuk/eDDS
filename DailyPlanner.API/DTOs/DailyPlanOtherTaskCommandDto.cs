namespace DailyPlanner.API.DTOs
{
    public class DailyPlanOtherTaskCommandDto
    {
        public int Id { get; set; }
        public Guid LinkedTaskId { get; set; }
        public string? Comment { get; set; }
        public int? OwnerId { get; set; }
        public TimeOnly? StartTime { get; set; }
        public int? Duration { get; set; }
        public string? TimingComment { get; set; }
        public int RiskLevel { get; set; }
        public int Status { get; set; }
        public int DailyPlanId { get; set; }
        public bool IsDeleted { get; set; }
        public bool ToBeDeleted { get; set; }
    }
}

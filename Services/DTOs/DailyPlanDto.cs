using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DTOs
{
    public class DailyPlanDto
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateTime StartDateDt
        {
            get => new(StartDate.Year, StartDate.Month, StartDate.Day);
            set => StartDate = DateOnly.FromDateTime(value);
        }
        public int ProductionLineId { get; set; }
        public ProductionLineDto ProductionLine { get; set; } = null!;
        public string? Comment { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select plan type")]
        public int DailyPlanTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public List<DailyPlanDefectTaskDto> DailyPlanDefectTasks { get; set; } = null!;
        public List<DailyPlanCilTaskDto> DailyPlanCilTasks { get; set; } = null!;
        public List<DailyPlanClTaskDto> DailyPlanClTasks { get; set; } = null!;
        public List<DailyPlanOtherTaskDto> DailyPlanOtherTasks { get; set; } = null!;
        public List<DailyPlanPmTaskDto> DailyPlanPmTasks { get; set; } = null!;

        [JsonIgnore]
        public Dictionary<int, string> PlanTypes { get; } = new()
        {
            {0,string.Empty},
            {1, "Daily Plan"},
            {2, "Pit Stop"}
        };

        [JsonIgnore]
        public string DailyPlanTypeDescription => PlanTypes[DailyPlanTypeId];

        public void Initialize()
        {
            DailyPlanDefectTasks = new ();
            DailyPlanCilTasks = new();
            DailyPlanClTasks = new();
            DailyPlanOtherTasks = new();
            DailyPlanPmTasks = new();
        }
    }
}

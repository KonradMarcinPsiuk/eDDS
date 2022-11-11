using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class DailyPlanDefectTask
    {
        public int Id { get; set; }
        public Guid LinkedTaskId { get; set; }
        public string? Comment { get; set; }
        public int? OwnerId { get; set; }
        public TimeSpan? StartTime { get; set; }
        public int? Duration { get; set; }
        public string? TimingComment { get; set; }
        public int RiskLevel { get; set; }
        public int Status { get; set; }
        public int DailyPlanId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual DailyPlan DailyPlan { get; set; } = null!;
        public virtual Defect LinkedTask { get; set; } = null!;
        public virtual Person? Owner { get; set; }
    }
}

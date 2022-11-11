using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class DailyPlan
    {
        public DailyPlan()
        {
            DailyPlanCilTasks = new HashSet<DailyPlanCilTask>();
            DailyPlanClTasks = new HashSet<DailyPlanClTask>();
            DailyPlanDefectTasks = new HashSet<DailyPlanDefectTask>();
            DailyPlanOtherTasks = new HashSet<DailyPlanOtherTask>();
            DailyPlanPmTasks = new HashSet<DailyPlanPmTask>();
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public int ProductionLineId { get; set; }
        public string? Comment { get; set; }
        public int DailyPlanTypeId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ProductionLine ProductionLine { get; set; } = null!;
        public virtual ICollection<DailyPlanCilTask> DailyPlanCilTasks { get; set; }
        public virtual ICollection<DailyPlanClTask> DailyPlanClTasks { get; set; }
        public virtual ICollection<DailyPlanDefectTask> DailyPlanDefectTasks { get; set; }
        public virtual ICollection<DailyPlanOtherTask> DailyPlanOtherTasks { get; set; }
        public virtual ICollection<DailyPlanPmTask> DailyPlanPmTasks { get; set; }
    }
}

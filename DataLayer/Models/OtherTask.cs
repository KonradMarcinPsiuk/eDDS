using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class OtherTask
    {
        public OtherTask()
        {
            DailyPlanOtherTasks = new HashSet<DailyPlanOtherTask>();
        }

        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? Action { get; set; }
        public int Status { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int LineAreaId { get; set; }
        public bool IsDeleted { get; set; }
        public int Priority { get; set; }

        public virtual LineArea LineArea { get; set; } = null!;
        public virtual ICollection<DailyPlanOtherTask> DailyPlanOtherTasks { get; set; }
    }
}

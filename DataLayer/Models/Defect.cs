using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Defect
    {
        public Defect()
        {
            DailyPlanDefectTasks = new HashSet<DailyPlanDefectTask>();
        }

        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? Action { get; set; }
        public int Status { get; set; }
        public int SubTypeId { get; set; }
        public int? OwnerId { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int LineAreaId { get; set; }
        public bool IsDeleted { get; set; }
        public int Priority { get; set; }
        public bool FoundDuringCil { get; set; }
        public bool PmHelpNeeded { get; set; }
        public string? PmHelpText { get; set; }

        public virtual LineArea LineArea { get; set; } = null!;
        public virtual Person? Owner { get; set; }
        public virtual ICollection<DailyPlanDefectTask> DailyPlanDefectTasks { get; set; }
    }
}

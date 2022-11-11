using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class LineArea
    {
        public LineArea()
        {
            CilTasks = new HashSet<CilTask>();
            ClTasks = new HashSet<ClTask>();
            ComponentActions = new HashSet<ComponentAction>();
            Defects = new HashSet<Defect>();
            Losses = new HashSet<Loss>();
            OtherTasks = new HashSet<OtherTask>();
            PmTasks = new HashSet<PmTask>();
            Transformations = new HashSet<Transformation>();
        }

        public int Id { get; set; }
        public int ProductionLineId { get; set; }
        public string AreaName { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual ProductionLine ProductionLine { get; set; } = null!;
        public virtual ICollection<CilTask> CilTasks { get; set; }
        public virtual ICollection<ClTask> ClTasks { get; set; }
        public virtual ICollection<ComponentAction> ComponentActions { get; set; }
        public virtual ICollection<Defect> Defects { get; set; }
        public virtual ICollection<Loss> Losses { get; set; }
        public virtual ICollection<OtherTask> OtherTasks { get; set; }
        public virtual ICollection<PmTask> PmTasks { get; set; }
        public virtual ICollection<Transformation> Transformations { get; set; }
    }
}

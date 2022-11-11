using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Person
    {
        public Person()
        {
            ComponentActions = new HashSet<ComponentAction>();
            DailyPlanCilTasks = new HashSet<DailyPlanCilTask>();
            DailyPlanClTasks = new HashSet<DailyPlanClTask>();
            DailyPlanDefectTasks = new HashSet<DailyPlanDefectTask>();
            DailyPlanOtherTasks = new HashSet<DailyPlanOtherTask>();
            DailyPlanPmTasks = new HashSet<DailyPlanPmTask>();
            Defects = new HashSet<Defect>();
            Losses = new HashSet<Loss>();
            Departments = new HashSet<Department>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsRealPerson { get; set; }
        public string? EmailAddress { get; set; }

        public virtual ICollection<ComponentAction> ComponentActions { get; set; }
        public virtual ICollection<DailyPlanCilTask> DailyPlanCilTasks { get; set; }
        public virtual ICollection<DailyPlanClTask> DailyPlanClTasks { get; set; }
        public virtual ICollection<DailyPlanDefectTask> DailyPlanDefectTasks { get; set; }
        public virtual ICollection<DailyPlanOtherTask> DailyPlanOtherTasks { get; set; }
        public virtual ICollection<DailyPlanPmTask> DailyPlanPmTasks { get; set; }
        public virtual ICollection<Defect> Defects { get; set; }
        public virtual ICollection<Loss> Losses { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}

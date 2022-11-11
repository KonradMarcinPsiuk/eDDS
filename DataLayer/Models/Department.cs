using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Department
    {
        public Department()
        {
            ProductionLines = new HashSet<ProductionLine>();
            SafetyZoneTriggerQuestionDepartments = new HashSet<SafetyZoneTriggerQuestionDepartment>();
            SafetyZoneTriggers = new HashSet<SafetyZoneTrigger>();
            People = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string? DepartmentName { get; set; }
        public int ValueStreamId { get; set; }

        public virtual ValueStream ValueStream { get; set; } = null!;
        public virtual ICollection<ProductionLine> ProductionLines { get; set; }
        public virtual ICollection<SafetyZoneTriggerQuestionDepartment> SafetyZoneTriggerQuestionDepartments { get; set; }
        public virtual ICollection<SafetyZoneTrigger> SafetyZoneTriggers { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}

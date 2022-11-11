using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class SafetyZoneTriggerQuestionDepartment
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int SafetyZoneTriggerQuestionId { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual SafetyZoneTriggerQuestion SafetyZoneTriggerQuestion { get; set; } = null!;
    }
}

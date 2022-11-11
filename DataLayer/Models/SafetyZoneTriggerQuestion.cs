using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class SafetyZoneTriggerQuestion
    {
        public SafetyZoneTriggerQuestion()
        {
            SafetyZoneTriggerQuestionDepartments = new HashSet<SafetyZoneTriggerQuestionDepartment>();
        }

        public int Id { get; set; }
        public string? Question { get; set; }

        public virtual ICollection<SafetyZoneTriggerQuestionDepartment> SafetyZoneTriggerQuestionDepartments { get; set; }
    }
}

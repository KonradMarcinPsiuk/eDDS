using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class SafetyZoneTrigger
    {
        public SafetyZoneTrigger()
        {
            SafetyZoneTriggerAnswers = new HashSet<SafetyZoneTriggerAnswer>();
        }

        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public DateTime Date { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<SafetyZoneTriggerAnswer> SafetyZoneTriggerAnswers { get; set; }
    }
}

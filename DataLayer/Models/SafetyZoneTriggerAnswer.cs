using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class SafetyZoneTriggerAnswer
    {
        public int Id { get; set; }
        public string? QuestionText { get; set; }
        public bool Answer { get; set; }
        public int SafetyZoneTriggerId { get; set; }

        public virtual SafetyZoneTrigger SafetyZoneTrigger { get; set; } = null!;
    }
}

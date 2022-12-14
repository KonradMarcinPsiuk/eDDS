using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class DailyTriggerQuestion
    {
        public DailyTriggerQuestion()
        {
            ProductionLineDailyTriggerQuestions = new HashSet<ProductionLineDailyTriggerQuestion>();
        }

        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public string? Hint { get; set; }
        public int Type { get; set; }
        public int TargetInt { get; set; }
        public int Field { get; set; }

        public virtual ICollection<ProductionLineDailyTriggerQuestion> ProductionLineDailyTriggerQuestions { get; set; }
    }
}

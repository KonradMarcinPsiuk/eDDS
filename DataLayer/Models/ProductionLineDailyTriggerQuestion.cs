using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class ProductionLineDailyTriggerQuestion
    {
        public int Id { get; set; }
        public int ProductionLineId { get; set; }
        public int DailyTriggerQuestionId { get; set; }

        public virtual DailyTriggerQuestion DailyTriggerQuestion { get; set; } = null!;
        public virtual ProductionLine ProductionLine { get; set; } = null!;
    }
}

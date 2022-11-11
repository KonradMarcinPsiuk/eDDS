using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class ProductionLine
    {
        public ProductionLine()
        {
            DailyPlans = new HashSet<DailyPlan>();
            DailyResults = new HashSet<DailyResult>();
            DailyTriggerAnswers = new HashSet<DailyTriggerAnswer>();
            LineAreas = new HashSet<LineArea>();
            ProductionLineDailyTriggerQuestions = new HashSet<ProductionLineDailyTriggerQuestion>();
        }

        public int Id { get; set; }
        public string LineName { get; set; } = null!;
        public int? ProficyLine { get; set; }
        public int? ProficyUnit { get; set; }
        public int DepartmentId { get; set; }
        public int? IsProficyBased { get; set; }
        public int? FamilyId { get; set; }
        public decimal? TargetPr { get; set; }
        public decimal? TargetCo { get; set; }
        public decimal? TargetUpdt { get; set; }
        public decimal? TargetPdt { get; set; }
        public decimal? TargetWaste { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<DailyPlan> DailyPlans { get; set; }
        public virtual ICollection<DailyResult> DailyResults { get; set; }
        public virtual ICollection<DailyTriggerAnswer> DailyTriggerAnswers { get; set; }
        public virtual ICollection<LineArea> LineAreas { get; set; }
        public virtual ICollection<ProductionLineDailyTriggerQuestion> ProductionLineDailyTriggerQuestions { get; set; }
    }
}

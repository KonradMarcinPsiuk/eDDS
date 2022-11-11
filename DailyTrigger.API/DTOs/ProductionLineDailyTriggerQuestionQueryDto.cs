namespace DailyTrigger.API.DTOs
{
    public class ProductionLineDailyTriggerQuestionQueryDto
    {
        public int Id { get; set; }
        public int ProductionLineId { get; set; }
        public int DailyTriggerQuestionId { get; set; }

        public virtual ProductionLineQueryDto ProductionLine { get; set; } = null!;
    }
}

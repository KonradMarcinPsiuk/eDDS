using System.Text.Json.Serialization;

namespace DTOs
{
    public class ProductionLineDailyTriggerQuestionDto
    {

        [JsonInclude]
        public int Id { get; private set; }
        [JsonInclude]
        public int ProductionLineId { get; private set; }
        [JsonInclude]
        public int DailyTriggerQuestionId { get; private set; }

        [JsonInclude]
        public ProductionLineDto ProductionLine { get; private set; } = null!;

        public void AddNewLink(DailyTriggerQuestionDto question, ProductionLineDto line)
        {
            ProductionLine = line;
            ProductionLineId = line.Id;
            DailyTriggerQuestionId = question.Id;
        }
    }
}

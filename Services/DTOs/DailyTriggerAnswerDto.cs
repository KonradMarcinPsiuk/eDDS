using System.Text.Json.Serialization;

namespace DTOs
{
    public class DailyTriggerAnswerDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = null!;
        public string? HintText { get; set; }
        public int QuestionType { get; set; }
        public int? TargetInt { get; set; }
        public int ProductionLineId { get; set; }
        public DateTime Date { get; set; }
        public string? AnswerTextDay { get; set; }
        public string? AnswerTextEve { get; set; }
        public string? AnswerTextNight { get; set; }
        public int? AnswerIntDay { get; set; }
        public int? AnswerIntEve { get; set; }
        public int? AnswerIntNight { get; set; }
        public int? AnswerTriggerDay { get; set; }
        public int? AnswerTriggerEve { get; set; }
        public int? AnswerTriggerNight { get; set; }
        public int Field { get; set; }

        [JsonIgnore]
        public Dictionary<int, string> Targets
        {
            get
            {
                return QuestionType switch
                {
                    1 => new Dictionary<int, string>()
                    {
                        {0, ""},{1, "Low"}, {2, "Medium"}, {3, "High"}
                    },

                    2 => new Dictionary<int, string>()
                    {
                        {0, ""},{1, "Yes"}, {2, "No"}
                    },

                    _ => new Dictionary<int, string>()


                };
            }
        }
        
        [JsonIgnore]
        public Dictionary<int, string> Fields = new()
        {
            {1, "Safety"}, {2, "Quality"}, {3, "Other"}
        };
        
        [JsonIgnore]
        public string FieldDescription => Fields[Field];
    }
}

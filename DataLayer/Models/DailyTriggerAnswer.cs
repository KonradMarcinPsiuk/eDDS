using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class DailyTriggerAnswer
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

        public virtual ProductionLine ProductionLine { get; set; } = null!;
    }
}

﻿namespace DailyTrigger.API.DTOs
{
    public class DailyTriggerQuestionQueryDto
    {
        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public string? Hint { get; set; }
        public int Type { get; set; }
        public int TargetInt { get; set; }
        public int Field { get; set; }


        public List<ProductionLineDailyTriggerQuestionQueryDto> ProductionLineDailyTriggerQuestions { get; set; }
    }
}
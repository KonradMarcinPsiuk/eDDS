namespace ZoneTrigger.API.DTOs;

public class SafetyZoneTriggerAnswerQueryDto
{
    public int Id { get; set; }
    public string? QuestionText { get; set; }
    public bool Answer { get; set; }
    public int SafetyZoneTriggerId { get; set; }

}
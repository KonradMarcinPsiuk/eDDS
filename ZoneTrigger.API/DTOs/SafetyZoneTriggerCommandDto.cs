namespace ZoneTrigger.API.DTOs;

public class SafetyZoneTriggerCommandDto
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public DateTime Date { get; set; }
    public  SafetyZoneTriggerAnswerCommandDto[] SafetyZoneTriggerAnswers { get; set; }
}
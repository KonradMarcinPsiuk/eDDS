namespace ZoneTrigger.API.DTOs;

public class SafetyZoneTriggerQueryDto
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public DateTime Date { get; set; }
    public  DepartmentQueryDto Department { get; set; } = null!;
    public  SafetyZoneTriggerAnswerQueryDto[] SafetyZoneTriggerAnswers { get; set; }
}
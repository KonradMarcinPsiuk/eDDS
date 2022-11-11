namespace DTOs;

public class SafetyZoneTriggerDto
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public DateTime Date { get; set; }
    public  DepartmentDto Department { get; set; } = null!;
    public  IEnumerable<SafetyZoneTriggerAnswerDto> SafetyZoneTriggerAnswers { get; set; }
}
namespace ZoneTrigger.API.DTOs;

public class SafetyZoneTriggerQuestionQueryDto
{
    public int Id { get; set; }
    public string? Question { get; set; }

    public  SafetyZoneTriggerQuestionDepartmentQueryDto[] SafetyZoneTriggerQuestionDepartments { get; set; }
}
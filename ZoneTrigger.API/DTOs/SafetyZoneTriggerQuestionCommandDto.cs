namespace ZoneTrigger.API.DTOs;

public class SafetyZoneTriggerQuestionCommandDto
{
    public int Id { get; set; }
    public string? Question { get; set; }

    public  SafetyZoneTriggerQuestionDepartmentCommandDto[] SafetyZoneTriggerQuestionDepartments { get; set; }
}
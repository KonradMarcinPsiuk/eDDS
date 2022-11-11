namespace ZoneTrigger.API.DTOs;

public class SafetyZoneTriggerQuestionDepartmentQueryDto
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public int SafetyZoneTriggerQuestionId { get; set; }

    public virtual DepartmentQueryDto Department { get; set; } = null!;
}
namespace DTOs;

public class SafetyZoneTriggerQuestionDepartmentDto
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public int SafetyZoneTriggerQuestionId { get; set; }
    public DepartmentDto Department { get; set; } = null!;

    public void AddNewLink(SafetyZoneTriggerQuestionDto question, DepartmentDto departmentDto)
    {
        SafetyZoneTriggerQuestionId = question.Id;
        Department = departmentDto;
        DepartmentId = departmentDto.Id;
    }
}
using System.ComponentModel.DataAnnotations;

namespace DTOs;

public class SafetyZoneTriggerQuestionDto
{
    public int Id { get; set; }
    [Required]
    public string? Question { get; set; }

    public  List<SafetyZoneTriggerQuestionDepartmentDto> SafetyZoneTriggerQuestionDepartments { get; set; }

    public void AddNewLine(SafetyZoneTriggerQuestionDepartmentDto link)
    {
        SafetyZoneTriggerQuestionDepartments.Add(link);
    }
}
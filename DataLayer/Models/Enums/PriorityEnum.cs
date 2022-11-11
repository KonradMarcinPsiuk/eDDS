using System.ComponentModel;

namespace DataLayer.Models.Enums;

public enum PriorityEnum
{
    [Description("Low")]
    Low=1,
    
    [Description("Medium")]
    Medium=2,
    
    [Description("High")]
    High=3
}
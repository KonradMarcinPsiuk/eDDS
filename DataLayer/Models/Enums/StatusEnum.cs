using System.ComponentModel;

namespace DataLayer.Models.Enums;

public enum StatusEnum
{
    [Description("Open")]
    Open=1,
    
    [Description("Closed")]
    Closed=2,
    
    [Description("Work in progress")]
    WorkInProgress=3
}
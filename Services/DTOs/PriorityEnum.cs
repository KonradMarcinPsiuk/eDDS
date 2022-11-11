﻿using System.ComponentModel;

namespace DTOs;

public enum PriorityEnum
{
    [Description("Low")]
    Low=1,
    
    [Description("Medium")]
    Medium=2,
    
    [Description("High")]
    High=3
}
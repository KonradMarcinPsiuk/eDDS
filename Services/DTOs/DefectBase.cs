using System.ComponentModel;


namespace DTOs;


public enum DefectSubtypeEnum
{
    [Description("Minor Defect")]
    MinorDefect =1,
    [Description("Lack of base condition")]
    LackOfBaseCondition=2,
    [Description("Hard to reach area")]
    HardToReachArea =3,
    [Description("Source of contamination")]
    SourceOfContamination=4,
    [Description("Quality defect")]
    QualityDefect=5,
    [Description("Unnecessary item")]
    UnnecessaryItem=6,
    [Description("Safety")]
    Safety=7,
    [Description("Temporary Centerline")]
    TemporaryCenterline=8
}
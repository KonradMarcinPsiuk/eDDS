namespace Interfaces;

public interface IDefect<TLineArea,TPerson,TDepartment> where TLineArea :class, ILineArea where TPerson:class, IPerson<TDepartment> where TDepartment :class,IDepartment
{
    public Guid Id { get; set; }
    public int LineAreaId { get; set; }
    
    public string? Description { get; set; }
    public string? Action { get; set; }
    public int Status { get; set; }
    public int SubTypeId { get; set; }
    public int? OwnerId { get; set; }
    public TPerson? Owner { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }
    public TLineArea LineArea { get; set; }
    public bool IsDeleted { get; set; }
    public int Priority { get; set; }
    public bool FoundDuringCil { get; set; }
    public bool PmHelpNeeded { get; set; }
    public string? PmHelpText { get; set; }
    //public Dictionary<int,string> SubTypes { get; }
    //public Dictionary<int, string> Priorities { get; }
    //public Dictionary<int, string> Statuses { get;  }

}
using DataLayer.Models;
using DataLayer.Models.Enums;

namespace DataLayer.Interfaces;

public interface IPlannableAction
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? Action { get; set; }
    public StatusEnum Status { get; set; }
    public int SubTypeId { get; set; }
    public int? OwnerId { get; set; }
    public Person? Owner { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }
    public int LineAreaId { get; set; }
    public LineArea LineArea { get; set; }
    public bool IsDeleted { get; set; }
    public PriorityEnum Priority { get; set; }
}
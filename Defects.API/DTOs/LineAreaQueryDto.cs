using Interfaces;

namespace Defects.API.DTOs;

public class LineAreaQueryDto:ILineArea
{
    public int Id { get; set; }
    public int ProductionLineId { get; set; }
    public string AreaName { get; set; } = null!;
    public bool IsDeleted { get; set; }
}
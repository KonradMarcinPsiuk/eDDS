namespace PlannedStop.API.DTOs
{
    public class LineAreaQueryDto
    {
        public int Id { get; set; }
        public int ProductionLineId { get; set; }
        public string AreaName { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}

namespace DTOs
{
    public interface IPlannedTask
    {
        public void SetAsNew();
        public LineAreaDto LineArea { get; set; }
        public int LineAreaId { get; set; }

        public Dictionary<int, string> Statuses { get; }

        public Dictionary<int, string> Priorities { get; }
        public string? Description { get; set; }
        public string? Action { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public string PriorityDescription { get; }
        public string StatusDescription { get; }
        public void CloseTask();
        public void OpenTask();
        public bool IsClosed { get; set; }


    }
}
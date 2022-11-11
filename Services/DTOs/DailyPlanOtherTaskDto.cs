namespace DTOs
{
    public class DailyPlanOtherTaskDto
    {
        public bool IsClosed
        {
            get => Status == 2;
            set => Status = value ? 2 : 1;
        }
        public int Id { get; set; }
        public Guid LinkedTaskId { get; set; }
        public string? Comment { get; set; }
        public int? OwnerId { get; set; }
        public TimeOnly? StartTime { get; set; }
        public int? Duration { get; set; }
        public string? TimingComment { get; set; }
        public int RiskLevel { get; set; }
        public int Status { get; set; }
        public int DailyPlanId { get; set; }
        public bool IsDeleted { get; set; }
        public OtherTaskDto LinkedTask { get; set; } = null!;

        public PersonDto? Owner { get; set; }
        public void SetLinkedTask(OtherTaskDto? d)
        {
            LinkedTask = d;
            LinkedTaskId = d.Id;
        }


        public DailyPlanOtherTaskDto()
        {
            StartTime = new TimeOnly(10, 0);
            RiskLevel = 1;
            Status = 1;
        }

        public void AssignOwner(PersonDto? owner)
        {
            if (owner != null)
            {
                Owner = owner;
                OwnerId = owner.Id;
            }
            else
            {
                Owner = null;
                OwnerId = null;
            }
        }

        public void SetStartTime(TimeOnly? time)
        {
            StartTime = time;
        }

    }
}

namespace DailyPlanner.API.DTOs
{
    public class PersonQueryDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsRealPerson { get; set; }
        public string? EmailAddress { get; set; }
    }
}


namespace DTOs
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsRealPerson { get; set; }
        public string? EmailAddress { get; set; }
        public List<DepartmentDto> Departments { get; set; }
        public string FullName => $"{LastName} {FirstName}";
    }
}

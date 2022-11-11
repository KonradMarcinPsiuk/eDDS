namespace DTOs
{
    public class ProductionLineDto
    {
        public int Id { get; set; }
        public string LineName { get; set; }
        public int? ProficyLine { get; set; }
        public int? ProficyUnit { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentDto Department { get; set; }
        public int? IsProficyBased { get; set; }
        public int? FamilyId { get; set; }
        public decimal? TargetPr { get; set; }
        public decimal? TargetCo { get; set; }
        public decimal? TargetUpdt { get; set; }
        public decimal? TargetPdt { get; set; }
        public decimal? TargetWaste { get; set; }
    }
}

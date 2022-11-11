﻿namespace DailyTrigger.API.DTOs
{
    public class ProductionLineQueryDto
    {
        public int Id { get; set; }
        public string LineName { get; set; } = null!;
        public int? ProficyLine { get; set; }
        public int? ProficyUnit { get; set; }
        public int DepartmentId { get; set; }
        public int? IsProficyBased { get; set; }
        public int? FamilyId { get; set; }
    }
}

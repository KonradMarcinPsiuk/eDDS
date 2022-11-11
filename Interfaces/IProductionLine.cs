using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IProductionLine
    {
        public int Id { get; set; }
        public string LineName { get; set; } 
        public int? ProficyLine { get; set; }
        public int? ProficyUnit { get; set; }
        public int DepartmentId { get; set; }
        public IDepartment Department { get; set; } 
        public int? IsProficyBased { get; set; }
        public int? FamilyId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDailyPlan
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public int ProductionLineId { get; set; }
        public IProductionLine ProductionLine { get; set; }
        public string? Comment { get; set; }
        public int DailyPlanTypeId { get; set; }
        public bool IsDeleted { get; set; }
        //public ICollection<DailyPlanDefectTaskDto> DailyPlanDefectTasks { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ILineArea
    {
        public int Id { get; set; }
        public int ProductionLineId { get; set; }
        public string AreaName { get; set; } 
        public bool IsDeleted { get; set; }

        //public ICollection<IComponentAction> ComponentActions { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class ValueStream
    {
        public ValueStream()
        {
            Departments = new HashSet<Department>();
        }

        public int Id { get; set; }
        public string ValueStreamName { get; set; } = null!;
        public int? PlantId { get; set; }

        public virtual Plant? Plant { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}

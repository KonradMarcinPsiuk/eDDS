using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Transformation
    {
        public Transformation()
        {
            Components = new HashSet<Component>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int LineAreaId { get; set; }

        public virtual LineArea LineArea { get; set; } = null!;
        public virtual ICollection<Component> Components { get; set; }
    }
}

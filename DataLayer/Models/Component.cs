using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Component
    {
        public Component()
        {
            ComponentActions = new HashSet<ComponentAction>();
        }

        public int Id { get; set; }
        public int? SapNumber { get; set; }
        public string? Manufacturer { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public int Type { get; set; }
        public int TransformationId { get; set; }
        public bool PmRequired { get; set; }
        public bool AmRequired { get; set; }
        public bool PmInPlace { get; set; }
        public bool AmInPlace { get; set; }
        public bool WearComponent { get; set; }
        public bool SpareRequired { get; set; }
        public bool SpareAvailable { get; set; }
        public bool ClRequired { get; set; }
        public bool ClInPlace { get; set; }

        public virtual Transformation Transformation { get; set; } = null!;
        public virtual ICollection<ComponentAction> ComponentActions { get; set; }
    }
}

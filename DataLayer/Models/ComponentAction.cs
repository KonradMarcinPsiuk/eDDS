using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class ComponentAction
    {
        public Guid Id { get; set; }
        public int ComponentId { get; set; }
        public string? Description { get; set; }
        public string? Action { get; set; }
        public int Status { get; set; }
        public int SubTypeId { get; set; }
        public int? OwnerId { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int LineAreaId { get; set; }
        public bool IsDeleted { get; set; }
        public int Priority { get; set; }

        public virtual Component Component { get; set; } = null!;
        public virtual LineArea LineArea { get; set; } = null!;
        public virtual Person? Owner { get; set; }
    }
}

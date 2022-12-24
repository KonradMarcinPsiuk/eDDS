using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ComponentActionDto
    {
        public int ComponentId { get; set; }
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? Action { get; set; }
        public int Status { get; set; }
        public int SubTypeId { get; set; }
        public PersonDto? Owner { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public LineAreaDto LineArea { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public int Priority { get; set; }
    }
}

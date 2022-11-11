using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string? DepartmentName { get; set; }
        public int ValueStreamId { get; set; }
        public ValueStreamDto ValueStream { get; set; } = null!;
    }
}

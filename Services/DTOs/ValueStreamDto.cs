using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ValueStreamDto
    {
        public int Id { get; set; }
        public string ValueStreamName { get; set; } = null!;
        public int? PlantId { get; set; }
    }
}

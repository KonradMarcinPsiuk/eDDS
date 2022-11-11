using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Plant
    {
        public Plant()
        {
            ValueStreams = new HashSet<ValueStream>();
        }

        public int Id { get; set; }
        public string PlantName { get; set; } = null!;

        public virtual ICollection<ValueStream> ValueStreams { get; set; }
    }
}

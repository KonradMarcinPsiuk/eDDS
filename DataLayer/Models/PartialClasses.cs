using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public partial class CilTask:ITask { }
    public partial class PmTask : ITask { }
    public partial class ClTask : ITask { }
    public partial class OtherTask : ITask { }
}

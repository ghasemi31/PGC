using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Patterns
{
    [Serializable]
    public class AgentBranchContactPattern : BasePattern
    {
        public string Name { get; set; }
        public DateRangePattern PersianDate { get; set; }
    }
}

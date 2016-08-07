using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Patterns
{
     [Serializable]
    public class BranchContactPattern : BasePattern
    {
        public BranchContactPattern()
        {
            Name = "";
            BranchTitle = "";
            PersianDate = new DateRangePattern();
        }
        public string Name { get; set; }
        public string BranchTitle { get; set; }
        public DateRangePattern PersianDate { get; set; }
        public long Branch_ID { get; set; }
    }
}
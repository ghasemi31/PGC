using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchSummaryChartPattern:BasePattern
    {
        public BranchSummaryChartPattern()
        {
            PersianDate = new DateRangePattern();
        }
        public bool IsActiveMinPrice { get; set; }
        public long MinPrice { get; set; }
        public DateRangePattern PersianDate { get; set; }
    }
}

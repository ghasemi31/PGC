using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchesOrderReportPattern : BasePattern
    {
        public BranchesOrderReportPattern()
        {
            PersianDate = new DateRangePattern();
        }
        public string Title { get; set; }
        public long Branch_ID { get; set; }
        public BranchOrderStatus Status { get; set; }
        public DateRangePattern PersianDate { get; set; }
    }
}

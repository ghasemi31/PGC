using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchOrderedTitlePattern:BasePattern
    {
        public BranchOrderedTitlePattern()
        {
            PersianDate = new DateRangePattern();
        }
        public long OrderTitle_ID { get; set; }
        public long Branch_ID { get; set; }
        public long GroupTitle_ID { get; set; }
        public DateRangePattern PersianDate { get; set; }
        public BranchOrderedTitleStatus Status { get; set; }
    }
}
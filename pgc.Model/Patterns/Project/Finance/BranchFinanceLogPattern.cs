using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchFinanceLogPattern:BasePattern
    {
        public BranchFinanceLogPattern()
        {
            Title = "";
            PersianDate = new DateRangePattern();
        }
        public long ID { get; set; }
        public long Branch_ID { get; set; }
        public long LogType_ID { get; set; }
        public string Title { get; set; }
        public BranchFinanceLogType LogType { get; set; }
        public BranchFinanceLogActionType ActionType { get; set; }
        public DateRangePattern PersianDate { get; set; }
    }
}
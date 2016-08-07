using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class LotteryPattern:BasePattern
    {
        public LotteryPattern()
        {
            RegPersianDate = new DateRangePattern();
            Title = "";
        }
        public string Title { get; set; }
        public DateRangePattern RegPersianDate { get; set; }
        public LotteryStatus Status { get; set; }
    }
}
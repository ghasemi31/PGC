using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class SentEmailBlockPattern : BasePattern
    {
        public SentEmailBlockPattern()
        {
            Title = "";
            RecipientMailAddress = "";
            PersianDate = new DateRangePattern();
            Size = new NumericRangePattern();
        }

        public string Title { get; set; }
        public string EventTitle { get; set; }
        public EventType EventType { get; set; }
        public long ID { get; set; }
        public long EmailSentAttempt_ID { get; set; }
        public NumericRangePattern Size { get; set; }
        public DateRangePattern PersianDate { get; set; }
        public SentEmailBlockStatus IsSent { get; set; }
        public string RecipientMailAddress { get; set; }
    }
}
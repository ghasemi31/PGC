using System;
using pgc.Model.Enums;

namespace pgc.Model.Patterns
{
    [Serializable]
    public class EmailSendAttemptPattern : BasePattern
    {
        public EmailSendAttemptPattern()
        {
            PersianDate = new DateRangePattern();
            SentEmail_Count = new NumericRangePattern();
            FailedEmail_Count = new NumericRangePattern();
            TotalEmail_Count = new NumericRangePattern();
            SentBlock_Count = new NumericRangePattern();
            FailedBlock_Count = new NumericRangePattern();
            TotalBlock_Count = new NumericRangePattern();
            BlockSize = new NumericRangePattern();
            InvalidEmailAddress_Count = new NumericRangePattern();

            Title = "";
            Recipient = "";
            Time = "";
            EventTitle = "";
        }

        public long ID { get; set; }
        public string Title { get; set; }
        public string Recipient { get; set; }
        public string Time { get; set; }
        public string EventTitle { get; set; }

        public EmailSendAttemptStatus Status { get; set; } 

        public EventType EventType { get; set; }
        public long OccuredEventID { get; set; }
        public DateTime Date { get; set; }
        public DateRangePattern PersianDate { get; set; }
        public BooleanStatus Has_File { get; set; }
        public NumericRangePattern SentEmail_Count { get; set; }
        public NumericRangePattern FailedEmail_Count { get; set; }
        public NumericRangePattern TotalEmail_Count { get; set; }
        public NumericRangePattern SentBlock_Count { get; set; }
        public NumericRangePattern FailedBlock_Count { get; set; }
        public NumericRangePattern TotalBlock_Count { get; set; }
        public NumericRangePattern BlockSize { get; set; }
        public NumericRangePattern InvalidEmailAddress_Count { get; set; }
    }
}
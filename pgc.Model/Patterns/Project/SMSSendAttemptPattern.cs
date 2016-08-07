using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class SMSSendAttemptPattern : BasePattern
    {
        
        public SMSSendAttemptPattern()
        {
            Message = "";
            EventTitle = "";
            PersianDate = new DateRangePattern();
            RecipientNumber = "";
            Time = "";

            Total_ErrorCount = new NumericRangePattern();
            Total_FailedCount = new NumericRangePattern();
            Total_SucceedCount = new NumericRangePattern();
            Total_SumCount = new NumericRangePattern();
            Total_UnknownCount = new NumericRangePattern();
        }

        public string Message { get; set; }
        public long ID { get; set; }

        public SMSSendAttemptStatus Status { get; set; }
        public string EventTitle { get; set; }
        public EventType EventType { get; set; }
        public long OccuredEventID { get; set; }

        public DateRangePattern PersianDate { get; set; }
        public NumericRangePattern Total_SucceedCount { get; set; }
        public NumericRangePattern Total_FailedCount { get; set; }
        public NumericRangePattern Total_SumCount { get; set; }
        public NumericRangePattern Total_UnknownCount { get; set; }
        public MessageType MessageType { get; set; }
        public NumericRangePattern Total_ErrorCount { get; set; }
        public string RecipientNumber { get; set; }

        public long SMSCount { get; set; }
        public long PhoneDeliveryCount { get; set; }
        public long PhoneFailCount { get; set; }
        public long TeleDeliveryCount { get; set; }
        public long TeleFailCount { get; set; }

        public string Time { get; set; }
    }
}
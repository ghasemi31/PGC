using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class SentSMSPattern:BasePattern
    {
        public SentSMSPattern()
        {
            Message = "";
            RecipientNumber = "";
            SendDate = new DateRangePattern();
        }

        public string Message { get; set; }

        public MessageType MessageType { get; set; }

        public string RecipientNumber { get; set; }

        public SendSMSStatus SendStatus { get; set; }

        public DateRangePattern SendDate { get; set; }

        public long SMSSendAttempt_ID { get; set; }
    }
}
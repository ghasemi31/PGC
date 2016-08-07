using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class OccuredSystemEventPattern:BasePattern
    {
        public OccuredSystemEventPattern()
        {
            Time = "";
            PersianDate = new DateRangePattern();

        }

        public long ID { get; set; }
        public long SystemEvent_ID { get; set; }
        public string Time { get; set; }
        //public SystemEventKey SystemEventKey { get; set; }
        public DateRangePattern PersianDate { get; set; }

        public EventActionType ActionType { get; set; }
    }
}
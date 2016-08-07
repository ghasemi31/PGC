using System;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class PollChoisePattern:BasePattern
    {
        public string Title { get; set; }
        public long Poll_ID { get; set; }
    }
}
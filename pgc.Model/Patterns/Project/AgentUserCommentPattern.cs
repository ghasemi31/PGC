using System;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class AgentUserCommentPattern:BasePattern
    {
        public string Name { get; set; }
        public Enums.UserCommentStatus Status { get; set; }
        public Enums.UserCommentType Type { get; set; }   
        public DateRangePattern UCPersianDate { get; set; }
    }
}
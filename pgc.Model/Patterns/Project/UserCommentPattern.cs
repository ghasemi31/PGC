using System;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class UserCommentPattern:BasePattern
    {
        public UserCommentPattern()
        {
            Name = "";
            BranchTitle = "";
            UCPersianDate = new DateRangePattern();
        }
        public string Name { get; set; }
        public string BranchTitle { get; set; }
        public Enums.UserCommentStatus Status { get; set; }
        public Enums.UserCommentType Type { get; set; }   
        public DateRangePattern UCPersianDate { get; set; }
        public long Branch_ID { get; set; }
    }
}
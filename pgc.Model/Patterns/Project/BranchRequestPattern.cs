using System;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchRequestPattern:BasePattern
    {
        public BranchRequestPattern()
        {
            Name = "";
            Contact = "";
            Description = "";
            BRPersianDate = new DateRangePattern();
        }

        public string Name { get; set; }
        public Enums.UserCommentStatus Status { get; set; }
        public string Contact { get; set; }
        public string Description { get; set; }
        public DateRangePattern BRPersianDate { get; set; }
        
    }
}
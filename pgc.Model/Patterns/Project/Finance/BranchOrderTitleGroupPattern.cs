using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchOrderTitleGroupPattern:BasePattern
    {
        public BranchOrderTitleGroupPattern()
        {
            Title = "";
            
        }
        public long ID { get; set; }
        public string Title { get; set; }
    }
}
using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchOrderTitlePattern:BasePattern
    {
        public BranchOrderTitlePattern()
        {
            Title = "";
            Price = new NumericRangePattern();
        }
        public long ID { get; set; }
        public long Group_ID { get; set; }
        public string Title { get; set; }
        public BranchOrderTitleStatus Status { get; set; }
        public NumericRangePattern Price { get; set; }
    }
}
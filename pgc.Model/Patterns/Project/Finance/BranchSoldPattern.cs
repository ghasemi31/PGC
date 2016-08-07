using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchSoldPattern:BasePattern
    {
        public BranchSoldPattern()
        {
            Price = new NumericRangePattern();
            SoldPersianDate = new DateRangePattern();
        }
        public long Branch_ID { get; set; }
        public long OrderTitle_ID { get; set; }
        public long GroupTitle_ID { get; set; }
        public NumericRangePattern Price { get; set; }
        public DateRangePattern SoldPersianDate { get; set; }
        public BranchSoldType Type { get; set; }
    }
}
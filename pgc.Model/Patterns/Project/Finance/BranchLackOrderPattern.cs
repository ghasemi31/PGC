using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchLackOrderPattern:BasePattern
    {
        public BranchLackOrderPattern()
        {
            Title = "";
            Price = new NumericRangePattern();
            OrderedPersianDate = new DateRangePattern();
        }

        public long ID { get; set; }
        public long Branch_ID { get; set; }
        public long BranchOrder_ID { get; set; }
        public long OrderTitle_ID { get; set; }
        public string Title { get; set; }
        public BranchLackOrderStatus Status { get; set; }
        public NumericRangePattern Price { get; set; }
        public DateRangePattern OrderedPersianDate { get; set; }
        public bool IsApproved { get; set; }
    }
}
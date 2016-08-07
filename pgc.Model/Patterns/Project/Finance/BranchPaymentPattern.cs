using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchPaymentPattern : BasePattern
    {
        public BranchPaymentPattern()
        {
            Title = "";
            Price = new NumericRangePattern();
            PayDate = new DateRangePattern();
        }

        public bool DefaultSearch { get; set; }
        public long ID { get; set; }
        public long Branch_ID { get; set; }
        public string Title { get; set; }
        public NumericRangePattern Price { get; set; }
        public DateRangePattern PayDate { get; set; }
        public DateRangePattern RegDate { get; set; }
        public BranchPaymentTypeForSearch Type { get; set; }
    }
}
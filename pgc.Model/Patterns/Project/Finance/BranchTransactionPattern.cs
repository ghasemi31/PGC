using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchTransactionPattern:BasePattern
    {
        public BranchTransactionPattern()
        {
            Title = "";
            BranchCreditPrice = new NumericRangePattern();
            BranchDebtPrice = new NumericRangePattern();
            PersianDate = new DateRangePattern();
        }
        public long ID { get; set; }
        public long Branch_ID { get; set; }
        public string Title { get; set; }
        public BranchTransactionTypeForSearch Type { get; set; }
        public NumericRangePattern BranchCreditPrice { get; set; }
        public NumericRangePattern BranchDebtPrice { get; set; }
        public DateRangePattern PersianDate { get; set; }
    }
}
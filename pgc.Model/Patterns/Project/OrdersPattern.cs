using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class OrdersPattern:BasePattern
    {
        public OrdersPattern()
        {
            OrderPersianDate = new DateRangePattern();
            Amount = new NumericRangePattern();
            BranchTitle = "";
            RefNum = "";
            UserName = "";
        }

        public string BranchTitle { get; set; }
        public int Numbers { get; set; }
        public OrderStatus Status { get; set; }
        public DateRangePattern OrderPersianDate { get; set; }
        public NumericRangePattern Amount { get; set; }
        public long Branch_ID { get; set; }
        public long Product_ID { get; set; }
        public long ID { get; set; }
        public string RefNum { get; set; }
        public OrderPaymentStatus OrderPaymentStatus { get; set; }

        public string UserName { get; set; }

    }
}
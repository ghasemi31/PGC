using System;
using pgc.Model.Enums;

namespace pgc.Model.Patterns
{
    [Serializable]
    public class OrdersAgentPattern:BasePattern
    {

        public OrdersAgentPattern()
        {
            OrderPersianDate = new DateRangePattern();
            Amount = new NumericRangePattern();

            RefNum = "";
            UserName = "";
        }
        public int Numbers { get; set; }
        public Enums.OrderStatus Status { get; set; }
        public Enums.PaymentType PaymentType { get; set; }
        public DateRangePattern OrderPersianDate { get; set; }
        public NumericRangePattern Amount { get; set; }
        public long Product_ID { get; set; }

        public string UserName;
        public string RefNum;
        public OnlineTransactionStatus OnlineTransactionStatus { get; set; }

        public OrderPaymentStatus OrderPaymentStatus { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pgc.Model.Enums;

namespace pgc.Model.Patterns
{
    [Serializable]
    public class PaymentPattern:BasePattern
    {
        public PaymentPattern()
        {
            PersianDate = new DateRangePattern();
            Amount = new NumericRangePattern();
        }

        public long Game_ID { get; set; }
        public long Order_ID { get; set; }
        public long User_ID { get; set; }
        public GameOrderPaymentStatus Status { get; set; }
        public DateRangePattern PersianDate { get; set; }
        public NumericRangePattern Amount { get; set; }
    }
}

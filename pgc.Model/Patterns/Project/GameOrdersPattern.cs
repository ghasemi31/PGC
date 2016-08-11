using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class GameOrdersPattern:BasePattern
    {
        public GameOrdersPattern()
        {
            OrderPersianDate = new DateRangePattern();
            Amount = new NumericRangePattern();
            GameTitle = "";
            RefNum = "";
            UserName = "";
        }

        public string GameTitle { get; set; }
        public int Numbers { get; set; }
        public DateRangePattern OrderPersianDate { get; set; }
        public NumericRangePattern Amount { get; set; }
        public long Game_ID { get; set; }
        public long ID { get; set; }
        public string RefNum { get; set; }


        public string UserName { get; set; }

        public GameOrderPaymentStatus GameOrderPaymentStatus { get; set; }
    }
}
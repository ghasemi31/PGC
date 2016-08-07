using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Patterns
{
    [Serializable]
    public class ProductSoldChartPattern:BasePattern
    {
        public ProductSoldChartPattern()
        {
            PersianDate = new DateRangePattern();
        }
        public long Branch_ID { get; set; }
        public long Product_ID { get; set; }
        public long ProductGroup_ID { get; set; }
        public DateRangePattern PersianDate { get; set; }
    }
}

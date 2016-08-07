using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchManualChargePattern : BasePattern
    {
        public BranchManualChargePattern()
        {
            PersianDate = new DateRangePattern();
        }

        public long Branch_ID { get; set; }
        public BranchCreditStatus Status { get; set; }
        public DateRangePattern PersianDate { get; set; }
    }
}
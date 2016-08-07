using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchCreditPattern : BasePattern
    {
        public BranchCreditPattern()
        {
        }

        public long Branch_ID { get; set; }
        public BranchCreditStatus Status { get; set; }
    }
}
using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BankAccountPattern : BasePattern
    {
        public BankAccountPattern()
        {
            Description = "";
            Title = "";
        }

        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public OfflineBankAccountStatus Status { get; set; }
    }
}
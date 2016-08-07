using System;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class LotteryDetailPattern:BasePattern
    {
        public string Name { get; set; }
        public long Lottery_ID { get; set; }
        public int Code { get; set; }
    }
}
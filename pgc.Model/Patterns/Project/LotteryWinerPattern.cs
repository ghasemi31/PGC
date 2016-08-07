using System;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class LotteryWinerPattern:BasePattern
    {
        public string Name { get; set; }
        public long Lottery_ID { get; set; }
        public int Rank { get; set; }
    }
}
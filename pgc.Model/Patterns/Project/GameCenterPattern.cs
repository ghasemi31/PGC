using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class GameCenterPattern:BasePattern
    {
        public GameCenterPattern()
        {
            Description = "";

        }


        public long Province_ID { get; set; }
        public long City_ID { get; set; }
        public string Description { get; set; }
     
    }
}
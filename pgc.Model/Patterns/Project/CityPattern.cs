using System;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class CityPattern:BasePattern
    {
        public string Title { get; set; }
        public long Province_ID { get; set; }
    }
}
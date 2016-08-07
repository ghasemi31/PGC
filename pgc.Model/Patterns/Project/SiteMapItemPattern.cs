using System;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class SiteMapItemPattern : BasePattern
    {
        public string Title { get; set; }
        public long SiteMapCat_ID { get; set; }

    }
}
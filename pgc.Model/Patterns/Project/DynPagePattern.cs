using System;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class DynPagePattern:BasePattern
    {
        public string Title { get; set; }
        public string Meta { get; set; }
        public string Content { get; set; }
        public string UrlKey { get; set; }
        
    }
}
using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class NewsPattern:BasePattern
    {
        public NewsPattern()
        {
            Title = "";
        }
        public string Title { get; set; }
        public NewsStatus Status { get; set; }
    }
}
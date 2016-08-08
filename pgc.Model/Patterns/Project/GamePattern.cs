using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class GamePattern : BasePattern
    {
        public GamePattern()
        {
            Title = "";
            UrlKey = "";
        }

        public string Title { get; set; }
        public string UrlKey { get; set; }

    }
}
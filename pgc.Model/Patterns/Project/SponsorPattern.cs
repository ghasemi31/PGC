using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class SponsorPattern:BasePattern
    {
        public SponsorPattern()
        {
            Title = "";
        }
        public string Title { get; set; }
       
    }
}
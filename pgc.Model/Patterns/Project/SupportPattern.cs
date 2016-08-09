using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class SupportPattern:BasePattern
    {
        public SupportPattern()
        {
            Title = "";
        }
        public string Title { get; set; }
       
    }
}
using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class SlidePattern:BasePattern
    {
        public SlidePattern()
        {
            Title = "";
        }
        public string Title { get; set; }
       
    }
}
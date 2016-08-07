using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class ProductPattern:BasePattern
    {
        public ProductPattern()
        {
            Title = "";
        }
        public string Title { get; set; }
        public BooleanStatus AllowOnlineOrder { get; set; }
    }
}
using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchPattern : BasePattern
    {
        public BranchPattern()
        {
            Title = "";
            UrlKey = "";
            AllowOnlineOrderTimeFrom = "";
            AllowOnlineOrderTimeTo = "";
        }

        public string Title { get; set; }
        public string UrlKey { get; set; }
        public string AllowOnlineOrderTimeFrom { get; set; }
        public string AllowOnlineOrderTimeTo { get; set; }

        public BooleanStatus AllowOnlineOrder { get; set; }
    }
}
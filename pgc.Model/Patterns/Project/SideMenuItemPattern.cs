using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Patterns
{
    [Serializable]
    public class SideMenuItemPattern : BasePattern
    {
        public string Title { get; set; }
        public long SideMenuCat_ID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Other.Project
{
    [Serializable]

    public class ProductObject
    {
        public long ID { get; set; }
        public string ImageUrl { get; set; }
        public string BackImageUrl { get; set; }

        public string Title { get; set; }
        public long Price { get; set; }
        public string Desc { get; set; }
        public List<string> Materials { get; set; }

        public bool IsAccessories { get; set; }

        public bool AllowOrdering { get; set; }
       
    }
}

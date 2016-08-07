using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Other.Project
{
    [Serializable]

    public class OrderDetailObject
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public long UnitPrice { get; set; }
        public long SumPrice { get; set; }

        public int Quantity { get; set; }
      
        
    }
}

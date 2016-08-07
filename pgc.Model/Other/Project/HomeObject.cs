using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Other.Project
{
    [Serializable]

    public class HomeObject
    {


        public List<ProductObject> Products { get; set; }
        public List<NewsObject> News { get; set; }
        public List<string> Slider { get; set; }
        public string QualityCharter { get; set; }
        public string NavHeaderImageUrl { get; set; }
        public Dictionary<long, long> Prices { get; set; }
        public string OrderIsSuspendedMessage { get; set; }
        public HomeObject()
        {
            Products = new List<ProductObject>();
            News = new List<NewsObject>();
            Slider = new List<string>();
            Prices = new Dictionary<long, long>();
        }


    }
}

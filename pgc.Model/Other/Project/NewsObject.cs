using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Other.Project
{
    [Serializable]

    public class NewsObject
    {
        public long ID { get; set; }
        public string ImageUrl { get; set; }
        public string Desc { get; set; }

        public string Body { get; set; }

        public string Date { get; set; }
        public string Title { get; set; }

        
    }
}

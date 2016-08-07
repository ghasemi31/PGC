using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Other.Project
{
    [Serializable]

    public class LocationObject
    {

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImagePath { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }


    }
}

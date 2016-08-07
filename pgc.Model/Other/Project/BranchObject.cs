using System;
using System.Collections.Generic;

namespace pgc.Model.Other.Project
{
    [Serializable]

    public class BranchObject
    {
   
        public long ID{ get; set; }
        public string Name{ get; set; }
        public string ImagePath { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address{ get; set; }
        public string Tel{ get; set; }
        public string HoursOrdering{ get; set; }
        public string HoursServingFood{ get; set; }
        public string NumberOfChair{ get; set; }
        public string TransportCost{ get; set; }
        public string CityCode { get; set; }
        public List<string> Slides { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public bool CanOrder { get; set; }

        public BranchObject()
        {
            this.Slides = new List<string>();
            this.PhoneNumbers = new List<string>();
        }

    }
}

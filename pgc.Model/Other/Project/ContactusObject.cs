using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Other.Project
{
    [Serializable]

    public class ContactusObject
    {

        public List<LocationObject> LocationsList{ get; set; }
        public string ContactUsText{ get; set; }

        public Dictionary<long, string> FeedbackTypesList { get; set; }

        public Dictionary<long, string> BranchesList { get; set; }

        public ContactusObject()
        {
            this.LocationsList = new List<LocationObject>();
            this.BranchesList = new Dictionary<long, string>();
            this.FeedbackTypesList = new Dictionary<long, string>();
        }

    }
}

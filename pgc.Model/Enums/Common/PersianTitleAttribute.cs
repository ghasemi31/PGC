using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [AttributeUsage(AttributeTargets.All)]
    public class PersianTitleAttribute:Attribute
    {

        private string PersianString;

        public PersianTitleAttribute(string PersianString)
        {
            this.PersianString = PersianString;           
        }

        public override string ToString()
        {
            return this.PersianString;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pgc.Model.Enums;

namespace pgc.Model
{
    [Serializable]
    public class NumericRangePattern
    {
        public RangeType Type { get; set; }
        public long FirstNumber { get; set; }
        public long SecondNumber { get; set; }

        public bool HasFirstNumber { get; set; }
        public bool HasSecondNumber { get; set; }


    }
    public enum RangeType
    {
        [PersianTitle("هیچ")]
        Nothing = 0,
        [PersianTitle("برابر با")]
        EqualTo = 1,
        [PersianTitle("بیشتر از")]
        GreatherThan = 2,
        [PersianTitle("کمتر از")]
        LessThan = 3,
        [PersianTitle("ما بین")]
        Between = 4
    }

}

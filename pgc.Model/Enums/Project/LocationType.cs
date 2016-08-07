using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع مکان مورد نظر")]
    public enum LocationType
    {
       [PersianTitle("شخصی")]
        Personal = 1,
       [PersianTitle("استیجاری")]
        Leased = 2
    }
}

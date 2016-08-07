using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("جنسیت")]
    public enum Gender
    {
       [PersianTitle("مرد")]
        Male = 1,
       [PersianTitle("زن")]
        Female = 2
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نقش")]
    public enum EnumSample
    {
       [PersianTitle("کاربر")]
        User = 1,
       [PersianTitle("مدیر سیستم")]
        Admin = 2
    }
}

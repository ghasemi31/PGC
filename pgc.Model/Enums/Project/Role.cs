using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نقش")]
    public enum Role
    {
        [PersianTitle("کاربر")]
        User = 1,
        [PersianTitle("مدیر سیستم")]
        Admin = 2,
        [PersianTitle("مدیر بازی")]
        Agent = 3
    }
}

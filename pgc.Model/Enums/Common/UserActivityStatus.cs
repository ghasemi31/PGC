using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت کاربر")]
    public enum UserActivityStatus
    {
       [PersianTitle("غیر فعال")]
        Disabled = 1,
       [PersianTitle("فعال")]
        Enabled = 2,
       [PersianTitle("در حال بررسی")]
        Pending = 3
    }
}

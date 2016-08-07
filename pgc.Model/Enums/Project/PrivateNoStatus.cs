using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت")]
    public enum PrivateNoStatus
    {
       [PersianTitle("فعال ")]
        Enabled = 1,
       [PersianTitle("غیرفعال")]
       Disabled = 2,
    }
}

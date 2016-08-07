using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت خبر")]
    public enum NewsStatus
    {
        [PersianTitle("نمایش")]
        Show = 1,
        [PersianTitle("پنهان")]
        Hide = 2,

    }
}

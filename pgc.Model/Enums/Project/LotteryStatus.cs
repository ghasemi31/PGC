using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("مضعیت قرعه کشی")]
    public enum LotteryStatus
    {
       [PersianTitle("در حال اجرا")]
        flow = 1,
       [PersianTitle("پایان یافته")]
        complete = 2
    }
}

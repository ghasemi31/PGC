using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع اقدام")]
    public enum EventActionType
    {
        [PersianTitle("ارسال پیامک")]
        SMS = 1,
        [PersianTitle("ارسال ایمیل")]
        Email = 2
    }
}

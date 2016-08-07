using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت ارسال ایمیل")]
    public enum SentEmailBlockStatus
    {
        [PersianTitle("ارسال شد")]
        Sent = 2,
        [PersianTitle("با مشکل مواجه شد")]
        NotSent = 1,
    }
}

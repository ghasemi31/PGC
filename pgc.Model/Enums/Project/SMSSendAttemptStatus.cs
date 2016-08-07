using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت ارسال پیامک")]
    public enum SMSSendAttemptStatus
    {
        [PersianTitle("تمامی پیامک ها ارسال شده")]
        AllSent = 1,
        [PersianTitle("تعدادی با مشکل مواجه شده")]
        SomeSent = 2,
        [PersianTitle("تمامی پیامک ها با مشکل مواجه شده")]
        NoSent = 3
    }
}

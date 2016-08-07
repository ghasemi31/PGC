using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت ارسال ایمیل")]
    public enum EmailSendAttemptStatus
    {
        [PersianTitle("تمامی ایمیل ها ارسال شده")]
        AllSent = 1,
        [PersianTitle("تعدادی با مشکل مواجه شده")]
        SomeSent = 2,
        [PersianTitle("تمامی ایمیل ها با مشکل مواجه شده")]
        NoSent = 3
    }
}

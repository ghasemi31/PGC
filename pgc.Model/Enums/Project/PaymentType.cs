using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت درخواست از شعبه")]
    public enum PaymentType
    {
       [PersianTitle("پرداخت حضوری")]
        Presence = 1,
       [PersianTitle("پرداخت آنلاین ")]
        Online = 2,
       //[PersianTitle("پرداخت Offline(اعلام واریز وجه)")]
       // Offline = 3
    }
}

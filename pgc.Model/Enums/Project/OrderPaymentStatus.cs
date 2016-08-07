using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت مالی سفارش")]
    public enum OrderPaymentStatus
    {
        [PersianTitle("پرداخت آنلاین")]
        Online = 1,
        [PersianTitle("پرداخت حضوری")]
        Presence = 2,
        [PersianTitle("پرداخت آنلاین(موفق)")]
        OnlineSucced = 3,
        [PersianTitle("پرداخت آنلاین(ناموفق)")]
        OnlineFailed = 4,
    }
}

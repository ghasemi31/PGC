using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت مالی سفارش")]
    public enum GameOrderPaymentStatus
    {
        [PersianTitle("پرداخت آنلاین(موفق)")]
        OnlineSucced = 3,
        [PersianTitle("پرداخت آنلاین(ناموفق)")]
        OnlineFailed = 4,
    }
}

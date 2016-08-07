using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت پرداخت آفلاین")]
    public enum BranchOfflinePaymentStatus
    {
        [PersianTitle("عدم تایید")]
        NotPaid = 1,
        [PersianTitle("تایید")]
        Paid = 2,
        [PersianTitle("در حال بررسی")]
        Pending = 3,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع تراکنش مالی شعبه")]
    public enum BranchPaymentTypeForSearch
    {
        [PersianTitle("پرداخت آنلاین")]
        Online = 1,
        [PersianTitle("پرداخت آنلاین (موفق)")]
        OnlineSucceed = 2,
        [PersianTitle("پرداخت آنلاین (نا موفق)")]
        OnlineNotSucceed = 3,
        [PersianTitle("پرداخت آفلاین")]
        Offline = 4,
        [PersianTitle("پرداخت آفلاین (موفق)")]
        OfflineSucceed = 5,
        [PersianTitle("پرداخت آفلاین (ناموفق)")]
        OfflineNotSuccedd = 6,

    }
}

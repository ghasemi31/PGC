using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("پرداخت کننده")]
    public enum OnlinePaymentStatus
    {
        [PersianTitle("کاربر")]
        User = 1,
        [PersianTitle("شعب")]
        Branches = 2
    }
}

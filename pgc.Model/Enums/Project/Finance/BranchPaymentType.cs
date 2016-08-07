using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع تراکنش مالی شعبه")]
    public enum BranchPaymentType
    {
        [PersianTitle("پرداخت آنلاین")]
        Online = 1,
        [PersianTitle("واریز حضوری وجه")]
        Offline = 2,
    }
}

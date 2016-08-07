using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع تراکنش مالی")]
    public enum BranchTransactionType
    {
        [PersianTitle("پرداخت آنلاین مشتری")]
        CustomerOnline = 1,
        [PersianTitle("فاکتور درخواست شعبه")]
        BranchOrder = 2,
        [PersianTitle("پرداخت توسط شعبه")]
        BranchPayment = 3,
        [PersianTitle("فاکتور مرجوعی شعبه")]
        BranchReturnOrder = 4,
        [PersianTitle("فاکتور کسری درخواست شعبه")]
        BranchLackOrder = 5,
        [PersianTitle("شارژ دستی")]
        ManualCharge = 6
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع تراکنش")]
    public enum BranchTransactionTypeForSearch
    {
        [PersianTitle("دریافتی مرکز")]
        CenterInDebt = 1,
        [PersianTitle("پرداختی مرکز")]
        CenterInCredit = 2,
        [PersianTitle("فاکتور درخواست")]
        BranchOrder = 3,
        [PersianTitle("فاکتور کسری")]
        BranchLackOrder = 4,
        [PersianTitle("فاکتور مرجوعی")]
        BranchReturnOrder = 5,
        [PersianTitle("پرداخت های شعبه")]
        BranchPayment = 6,
        [PersianTitle("پرداخت های شعبه (آنلاین)")]
        BranchPaymentOnline = 7,
        [PersianTitle("پرداخت های شعبه (آفلاین)")]
        BranchPaymentOffline = 8,
        [PersianTitle("پرداخت سفارش آنلاین (توسط مشتری)")]
        CustomerPaymentOnline = 9,
        [PersianTitle("شارژ دستی")]
        ManualCharge = 10
    }
}

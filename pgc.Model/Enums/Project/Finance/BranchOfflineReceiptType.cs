using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pgc.Model.Enums;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع پرداخت آفلاین")]
    public enum BranchOfflineReceiptType
    {
        [PersianTitle("حضوری")]
        Wire_Bank = 1,
        [PersianTitle("ساتنا")]
        Satna_Bank = 2,
        [PersianTitle("ساتنا اینترنتی")]
        Satna_Internet = 3,
        [PersianTitle("شتاب عابر بانک")]
        Shetab_ATM = 4,
        [PersianTitle("شتاب اینترنتی")]
        Shetab_Internet = 5
    }
}

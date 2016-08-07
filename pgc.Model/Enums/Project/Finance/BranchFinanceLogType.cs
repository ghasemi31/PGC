using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع فاکتور گزارش")]
    public enum BranchFinanceLogType
    {
        [PersianTitle("درخواست")]
        BranchOrder = 1,
        [PersianTitle("کسری")]
        BranchLackOrder = 2,
        [PersianTitle("مرجوعی")]
        BranchReturnOrder = 3
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت اعتبار شعبات")]
    public enum BranchCreditStatus
    {
        [PersianTitle("بدهکار")]
        InDebt = 1,
        [PersianTitle("بستانکار")]
        InCredit = 2,
    }
}

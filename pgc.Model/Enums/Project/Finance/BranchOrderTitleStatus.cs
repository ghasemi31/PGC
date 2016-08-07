using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت کالای سفارشی")]
    public enum BranchOrderTitleStatus
    {
       [PersianTitle("قابل سفارش دهی")]
        Enabled = 1,
       [PersianTitle("معلق")]
        Disabled = 2
    }
}

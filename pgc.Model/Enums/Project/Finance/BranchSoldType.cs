using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع فعالیت مالی")]
    public enum BranchSoldType
    {
        [PersianTitle("فروخته شده")]
        Sold = 1,
        [PersianTitle("مرجوع شده")]
        Rejected = 2,
    }
}

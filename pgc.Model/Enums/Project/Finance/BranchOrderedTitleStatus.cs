using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت کالای سفارشی")]
    public enum BranchOrderedTitleStatus
    {
        [PersianTitle("ارسال شده به شعبه")]
        Prepared = 1,
        [PersianTitle("فروخته شده")]
        Sold = 2,
        [PersianTitle("مرجوع شده")]
        Rejected = 3,
        [PersianTitle("کسری")]
        Lacked = 4,
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت کسری های شعبه")]
    public enum BranchLackOrderStatus
    {
        [PersianTitle("در حال بررسی")]
        Pending = 1,
        [PersianTitle("تایید")]
        Confirmed = 2,
        [PersianTitle("ابطال")]
        Canceled = 3,
        //[PersianTitle("بسته شده")]
        //Finalized,

    }
}
